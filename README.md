# React AWS S3 Upload Demo


## Table of Content
* [Setup](#Setup)
* [Images](#Images)

### Setup
Run the following to install node dependencies.
```sh
npm i
```
Add .env file for credentials in the directory and add the following with your AWS credentials
```sh
S3_BUCKET=
AWS_ACCESS_KEY_ID=
AWS_SECRET_ACCESS_KEY=
```
Add credentials for bitly in app.js
```js
const TOKEN = ''; // Enter you BITLY TOKEN 
```
To start the server, cd into the directory and run the following
```sh
nodemon
npm start
```
If nodemon cannot be accessed, run
```sh
npm install -g nodemon
```
### Images
## Home
![Images](https://github.com/Chemist-3/ST0280-Assignment-1/blob/T5/docs/home.png)
## Loading
![Images](https://github.com/Chemist-3/ST0280-Assignment-1/blob/T5/docs/loading.png)
## Success
![Images](https://github.com/Chemist-3/ST0280-Assignment-1/blob/T5/docs/uploaded.png)

