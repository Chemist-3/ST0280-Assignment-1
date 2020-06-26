import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';
import { v4 as randomString } from 'uuid';
import axios from 'axios';
import Dropzone from 'react-dropzone';
import { GridLoader } from 'react-spinners';

const TOKEN = 'ENTER VERYFI TOKEN HERE';

class App extends Component {
  constructor() {
    super();
    this.state = {
      isUploading: false,
      id: '',
      date: '',
      img_url: '',
      subtotal: '',
      total: '',
      tax: '',
      currency_code: '',
      obj: null,
    };
  }

  uploadFile = ([file]) => {
    this.setState({ isUploading: true });

    const config = {
      headers: {
        'CLIENT-ID': 'ENTER VERYFI CLIENT-ID HERE',
        'AUTHORIZATION': `apikey ${TOKEN}`,
        'Accept': 'application/json',
        'Content-Type': 'application/x-www-form-urlencoded',
      }
    };
    const file_name = `${randomString()}-${file.name.replace(/\s/g, '-')}`;

    const fd = new FormData();
    fd.append('file', file)
    fd.append('file_name', file_name)

    // IMPT TO RUN CHROME WITH TARGET  --disable-web-security --disable-gpu --user-data-dir=~/chromeTemp
    // to bypass CORS as server does not have access-control-allow-origin response headers
    axios.post('https://api.veryfi.com/api/v7/partner/documents/', fd, config)
      .then(response => {
        console.log(response);
        var object = "text/json;charset=utf-8," + encodeURIComponent(JSON.stringify(response.data));
        this.setState({
          isUploading: false,
          id: response.data.id,
          date: response.data.date,
          img_url: response.data.img_url,
          subtotal: response.data.subtotal,
          total: response.data.total,
          tax: response.data.tax,
          currency_code: response.data.currency_code,
          obj: object
        });
      }).catch(err => {
        console.log(err);
        this.setState({ isUploading: false });
      });
  }
  render() {
    const { isUploading, id, date, img_url, subtotal, total, currency_code, tax, obj } = this.state;
    return (
      <div className="App">
        <img src={img_url} alt="" width="450px" />
        <Dropzone
          className="dropzone"
          onDropAccepted={this.uploadFile}
          accept="image/*"
          multiple={false}>
          {({ getRootProps, getInputProps }) => (
            <section>
              <div {...getRootProps({ className: 'dropzone' })}>
                <input {...getInputProps()} />
                {isUploading ? <GridLoader /> : <p>Drop or Click to upload image Here</p>}
              </div>
            </section>
          )}
        </Dropzone>
        <div className="infomation">
          <h3>BASIC RECEIPT INFOMATION</h3>
          <p>ID: {id}</p>
          <p>DATE: {date}</p>
          <p>SUBTOTAL: {subtotal+currency_code}</p>
          <p>TAX: {tax+currency_code}</p>
          <p>TOTAL: {total+currency_code}</p>
          <a href={"data: " + obj} download="data.json">Download JSON</a>
        </div>
      </div>
    );
  }
}

export default App;
