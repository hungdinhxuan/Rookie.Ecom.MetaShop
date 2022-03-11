import React, {useEffect} from 'react';
import { useNavigate  } from 'react-router-dom';
import { CallbackComponent } from 'redux-oidc';
import userManager from '../utils/userManager';

const CallbackPage = () => {

    let navigate = useNavigate();
   
    const handleLoginSuccess = (user) => {
      localStorage.setItem('user', JSON.stringify(user));
      navigate('/dashboard/app');
    }

    useEffect(() => {
        const isLoggedIn = Boolean(localStorage.getItem('user'));
        if (isLoggedIn)
        {
            navigate('/dashboard/app');
        }
    }, [navigate]);

    return (
        <CallbackComponent
        userManager={userManager}
        successCallback={handleLoginSuccess}
        errorCallback={error => {
            navigate('/404');
          //this.props.dispatch(push('/'));
          console.error(error);
        }}
      >
        <div>Redirecting...</div>
      </CallbackComponent>
    )
}

export default CallbackPage
