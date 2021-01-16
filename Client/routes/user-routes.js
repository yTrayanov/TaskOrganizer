const express = require('express');
const userService = require('../services/userService');
const router = new express.Router();


router.get('/' ,async (req , res) =>{
    const isLogged = await userService.CheckIfLoggedIn();
    if(isLogged){
        return res.render('home');s
    } else {
        res.redirect('/login');
    }
    
})

router.get('/login' , async (req , res) =>{
    const isLogged = await userService.CheckIfLoggedIn();
    console.log(isLogged);
    if(isLogged){
        res.render('home');
        return;
    }

    res.render('login');
})
router.post('/login',async (req,res) =>{

    const hasLogged = await userService.login(req.body);
    if(hasLogged === true){
        res.render('home');
    }
    else{
        res.redirect('/login');
    }
})



module.exports = router;