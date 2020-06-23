import React, { Component } from 'react';
import './App.css';
import axios from 'axios';
import { v4 as randomString } from 'uuid';
import Dropzone from 'react-dropzone';
import { GridLoader } from 'react-spinners';

const TOKEN = ''; // Enter you BITLY TOKEN 

class App extends Component {
  constructor() {
    super();
    this.state = {
      isUploading: false,
      url: 'http://via.placeholder.com/450x450',
      shorturl: '',
    };
  }

  getSignedRequest = ([file]) => {
    this.setState({ isUploading: true });
    // We are creating a file name that consists of a random string, and the name of the file that was just uploaded with the spaces removed and hyphens inserted instead.
    // This is done using the .replace function with a specific regular expression. This will ensure that each file uploaded has a unique name which will prevent files from overwriting other files due to duplicate names.
    const fileName = `${randomString()}-${file.name.replace(/\s/g, '-')}`;

    // We will now send a request to our server to get a "signed url" from Amazon. We are essentially letting AWS know that we are going to upload a file soon.
    // We are only sending the file-name and file-type as strings. We are not sending the file itself at this point.
    axios
      .get('/api/signs3', {
        params: {
          'file-name': fileName,
          'file-type': file.type,
        },
      })
      .then(response => {
        const { signedRequest, url } = response.data;
        // call uploadFile
        this.uploadFile(file, signedRequest, url);
      })
      .catch(err => {
        console.log(err);
      });
  };

  uploadFile = (file, signedRequest, url) => {
    // setting req headers
    const options = {
      headers: {
        'Content-Type': file.type,
      },
    };

    axios
      .put(signedRequest, file, options)
      .then(response => {
        // call shortenURL to shorten the url recieved by Amazon via BITLY
        this.shortenURL(url);
      })
      .catch(err => {
        this.setState({
          isUploading: false,
        });
        if (err.response.status === 403) {
          alert(
            `Your request for a signed URL failed with a status 403. Double check the CORS configuration and bucket policy in the README. You also will want to double check your AWS_ACCESS_KEY_ID and AWS_SECRET_ACCESS_KEY in your .env and ensure that they are the same as the ones that you created in the IAM dashboard. You may need to generate new keys\n${
              err.stack
            }`
          );
        } else {
          alert(`ERROR: ${err.status}\n ${err.stack}`);
        }
      });
  };

  shortenURL = (url) => {
    // setting req headers for content type and authorization
    const options = {
      headers: {
        Authorization: `Bearer ${TOKEN}`,
        'Content-Type': 'application/json',
      },
    };
    axios
      .post('https://api-ssl.bitly.com/v4/shorten', 
      {
        "domain": "bit.ly",
        "long_url": url
      },
        options,
      ).then(response => {
        console.log(response)
        this.setState({ isUploading: false, url: response.data.long_url , shorturl: response.data.link });
      }).catch()
  };

  render() {
    const { url, isUploading, shorturl } = this.state;
    return (
      <div className="App">
        <h1>Upload your images to S3 Bucket!</h1>
        <img src={url} alt="" width="450px" />
        <h3>{url}</h3>
        <h3>{shorturl}</h3>
        
        <Dropzone
          className="dropzone"
          onDropAccepted={this.getSignedRequest}
          accept="image/*"
          multiple={false}>
          {({getRootProps, getInputProps}) => (
            <section>
              <div {...getRootProps({className: 'dropzone'})}>
                <input {...getInputProps()} />
                {isUploading ? <GridLoader /> : <p>Drop or Click to upload image Here</p>}
              </div>
            </section>
          )}
        </Dropzone>

      </div>
    );
  }
}

export default App;