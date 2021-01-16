

const CheckIfUserIsLogged =() => {
    let isLogged = fetch("https://localhost:44382/Home/CheckIfLoggedIn")
        .then(response => response.json)
        .then(data => console.log(data));
}

const fetch = require('node-fetch');



const login = () =>{

    const data ={ Username:'user1' , Password:'user123'};

    fetch("https://localhost:44382/Identity/Account/Login", {
        method:"POST",
        mode:"no-cors",
        headers:{
            'Content-Type': 'application/x-www-form-urlencoded',
        },
        body:JSON.stringify(data)
    }).then(res => console.log(res.status))
}




const GetTask = () =>{
    fetch("https://localhost:44382/Teamleader/Task/GetTask?id=1&type=Individual", {
        method:"GET",
        mode:"no-cors",
        headers:{
            'Content-Type': 'application/json',
        }
    }).then(res => {console.log(res.status)});
}

login();
CheckIfLoggedIn();