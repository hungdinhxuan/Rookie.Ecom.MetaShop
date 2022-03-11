import React, { useEffect } from 'react';
import { Button } from 'react-bootstrap';
import userManager from '../utils/userManager';


const handleLogin = () => {
    console.log('handleLogin');
    userManager.signinRedirect();
}

const Welcome = () => {
    useEffect(() => {
        const isLoggedIn = Boolean(localStorage.getItem('user'));
        if (isLoggedIn)
        {
            const user = JSON.parse(localStorage.getItem("user"));
            localStorage.removeItem('user');
            userManager.signoutRedirect({ id_token_hint: user.id_token });
            userManager.removeUser();
        }
      });
    return (
        <Button style={{
            margin: 'auto',
        }} onClick={() => handleLogin()}>Login</Button>
    )
}

export default Welcome
