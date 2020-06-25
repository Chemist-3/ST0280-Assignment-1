import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';
import { v4 as randomString } from 'uuid';
import axios from 'axios';
import Dropzone from 'react-dropzone';
import { GridLoader } from 'react-spinners';

const TOKEN = 'Enter your TOKEN';

class App extends Component {
  constructor() {
    super();
    this.state = {
      isUploading: false,
    };
  }

  uploadFile = ([file]) => {
    this.setState({ isUploading: true });
    const options = {
      headers:{
        'CLIENT-ID': 'ENTER CLIENT_ID HERE',
        'AUTHORIZATION': `apikey ${TOKEN}`,
        'Accept': 'application/json',
        'Content-Type': 'application/x-www-form-urlencoded',
      },
      onUploadProgress: progressEvent => {
        console.log('Upload Progress' + Math.round(progressEvent.loaded / progressEvent.total * 100) + '%')
      }
    }
    const file_name = `${randomString()}-${file.name.replace(/\s/g, '-')}`;

    const fd = new FormData();
    fd.append('image', file, file_name)

    axios
      .post('https://api.veryfi.com/api/v7/partner/documents/', 
      fd,
      options,).then(res => {
        console.log(res);
        this.setState({ isUploading: false });
      }).catch(err => {
        console.log(err);
        this.setState({ isUploading: false });
      })
  }
  render() {
    const {isUploading } = this.state;
    return (
      <div className="App">
        <Dropzone
          className="dropzone"
          onDropAccepted={this.uploadFile}
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
