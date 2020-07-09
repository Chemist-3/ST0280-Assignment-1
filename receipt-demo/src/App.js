import React, { Component } from "react";
import logo from "./logo.svg";
import "./App.css";
import { v4 as randomString } from "uuid";
import axios from "axios";
import Dropzone from "react-dropzone";
import { GridLoader } from "react-spinners";

class App extends Component {
  constructor() {
    super();
    this.state = {
      isUploading: false,
      id: "",
      date: "",
      img_url: "",
      subtotal: "",
      total: "",
      tax: "",
      currency_code: "",
      obj: null,
    };
  }

  uploadAsync = async ([file]) => {
    this.setState({ isUploading: true });

    const toBase64 = (file) =>
      new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result);
        reader.onerror = (error) => reject(error);
      });

    const file_name = `${randomString()}-${file.name.replace(/\s/g, "-")}`;
    const data = { file_name: file_name, file_data: await toBase64(file) };

    axios
      .post("/upload", data)
      .then((response) => {
        console.log(response);
        var object =
          "text/json;charset=utf-8," +
          encodeURIComponent(JSON.stringify(response.data));
        this.setState({ isUploading: false });
        this.setState({
          isUploading: false,
          id: response.data.id,
          date: response.data.date,
          img_url: response.data.img_url,
          subtotal: response.data.subtotal,
          total: response.data.total,
          tax: response.data.tax,
          currency_code: response.data.currency_code,
          obj: object,
        });
      })
      .catch((err) => {
        console.log(err);
        this.setState({ isUploading: false });
      });
  };
  render() {
    const {
      isUploading,
      id,
      date,
      img_url,
      subtotal,
      total,
      currency_code,
      tax,
      obj,
    } = this.state;
    return (
      <div className="App">
        <img src={img_url} alt="" width="450px" />
        <Dropzone
          className="dropzone"
          onDropAccepted={this.uploadAsync}
          accept="image/*"
          multiple={false}
        >
          {({ getRootProps, getInputProps }) => (
            <section>
              <div {...getRootProps({ className: "dropzone" })}>
                <input {...getInputProps()} />
                {isUploading ? (
                  <GridLoader />
                ) : (
                  <p>Drop or Click to upload image Here</p>
                )}
              </div>
            </section>
          )}
        </Dropzone>
        <div className="infomation">
          <h3>BASIC RECEIPT INFOMATION</h3>
          <p>ID: {id}</p>
          <p>DATE: {date}</p>
          <p>SUBTOTAL: {subtotal + currency_code}</p>
          <p>TAX: {tax + currency_code}</p>
          <p>TOTAL: {total + currency_code}</p>
          <a href={"data: " + obj} download="data.json">
            Download JSON
          </a>
        </div>
      </div>
    );
  }
}

export default App;
