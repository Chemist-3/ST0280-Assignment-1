const express = require('express'),
  app = express(),
  port = 3010;

app.use(express.json());


app.get('/upload', (req, res) => {
});

app.listen(port, () => console.log(`Listening on port ${port}`));