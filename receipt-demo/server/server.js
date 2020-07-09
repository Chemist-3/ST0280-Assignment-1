require("dotenv").config();
const axios = require("axios");
const express = require("express"),
  app = express(),
  port = 3010;

app.use(express.json({ limit: "10mb" }));

const { CLIENT_ID, AUTHORIZATION } = process.env;

app.post("/upload", (request, response) => {
  console.log(request.body.file_name);

  const config = {
    headers: {
      "CLIENT-ID": CLIENT_ID,
      AUTHORIZATION: AUTHORIZATION,
      Accept: "application/json",
      "Content-Type": "application/x-www-form-urlencoded",
    },
  };
  axios
    .post(
      "https://api.veryfi.com/api/v7/partner/documents/",
      request.body,
      config
    )
    .then((res) => {
      console.log("Success");
      response.send(res.body);
    })
    .catch((err) => {
      console.log(err);
      response.send(err.body);
    });
});

app.listen(port, () => console.log(`Server listening on port ${port}`));
