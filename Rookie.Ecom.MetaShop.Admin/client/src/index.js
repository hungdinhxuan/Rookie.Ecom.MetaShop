// scroll bar
import "simplebar/src/simplebar.css";
// import 'antd/dist/antd.css';
import "bootstrap/dist/css/bootstrap.min.css";
import ReactDOM from "react-dom";
import { BrowserRouter } from "react-router-dom";
import { HelmetProvider } from "react-helmet-async";

//
import App from "./App";
import * as serviceWorker from "./serviceWorker";
import reportWebVitals from "./reportWebVitals";
import { store } from "./app/store";
import { Provider } from "react-redux";
import userManager from "./utils/userManager";
import { OidcProvider } from "redux-oidc";

// ----------------------------------------------------------------------

ReactDOM.render(
  <Provider store={store}>
    <OidcProvider store={store} userManager={userManager}>
      <HelmetProvider>
        <BrowserRouter>
          <App />
        </BrowserRouter>
      </HelmetProvider>
    </OidcProvider>
  </Provider>,
  document.getElementById("root")
);

// If you want to enable client cache, register instead.
serviceWorker.unregister();

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
