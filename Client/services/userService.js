process.env['NODE_TLS_REJECT_UNAUTHORIZED'] = 0;
const fetch = require('node-fetch');

const CheckIfLoggedIn = async () =>{
    const isLogged = await fetch("https://localhost:44382/Home/CheckIfLoggedIn", {
        method:"GET",
        mode:"no-cors",
        headers:{
            'Content-Type': 'application/json',
        },
    }).then(res => {
        if(res.status === 200){
            return true;
        }
        else{
            return false;
        }
    }).then(data => data).catch(err =>{
        console.log(err)
    })

    return isLogged;
}

const login = async ({username , password}) =>{

    const hasLogged = await fetch(`https://localhost:44382/Identity/Account/Login?username=${username}&password=${password}`, {
        method:"POST",
        mode:"no-cors",
        headers:{
            'Content-Type': 'application/x-www-form-urlencoded',
        }
    }).then(res => {

        if(res.status === 200){
            return true;
        }
        else{
            return false;
        }
    }).then(data =>{
        return data
    });



    return hasLogged;
}

module.exports ={
    CheckIfLoggedIn,
    login
}