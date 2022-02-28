// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import { getAnalytics } from "firebase/analytics";
import {getStorage, ref} from "firebase/storage";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
  apiKey: "AIzaSyANfqE9tpQ1B3XTlodkQPZAr2Od3A506_Q",
  authDomain: "rookie-metashop.firebaseapp.com",
  projectId: "rookie-metashop",
  storageBucket: "rookie-metashop.appspot.com",
  messagingSenderId: "63908185895",
  appId: "1:63908185895:web:70ae2649344c81cfe30b02",
  measurementId: "G-0RWFEV85LC"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);
export const storage = getStorage(app);
const analytics = getAnalytics(app);