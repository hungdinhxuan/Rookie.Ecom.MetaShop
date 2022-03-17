import { createUserManager } from 'redux-oidc';

const frontend = process.env.REACT_APP_ADMIN_URL_END_POINT;
const identity = process.env.REACT_APP_IDENTITY_URL_END_POINT;

const userManagerConfig = {
    client_id: 'rookieecom',
    client_secret: 'rookieecom',
    redirect_uri: `${frontend}callback`,
    post_logout_redirect_uri: `${frontend}`,
    response_type: 'id_token token',
    scope: 'openid profile roles',
    authority: `${identity}`,
    silent_redirect_uri: `${frontend}silent_renew.html`,
    automaticSilentRenew: true,
    filterProtocolClaims: true,
    loadUserInfo: true,
    monitorSession: true,
    grantType: 'implicit'
    
};

const userManager = createUserManager(userManagerConfig);

export default userManager;
