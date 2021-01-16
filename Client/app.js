const express = require('express');
const exphbs  = require('express-handlebars');
const bodyparser = require('body-parser')
const userRoutes = require('./routes/user-routes');

const PORT = 8080;
const app = express();
 
app.engine('hbs', exphbs({defaultLayout:'main' , extname: '.hbs'}));
app.set('view engine', 'hbs');
app.use(express.static(__dirname + '/public'));
app.use(bodyparser.json());
app.use(bodyparser.urlencoded({extended:true}));

app.use('/' , userRoutes);
 
app.listen(PORT , () => {
    console.log(`Listening on port ${PORT}...`);
});