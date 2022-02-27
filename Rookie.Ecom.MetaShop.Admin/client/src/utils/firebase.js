import { initializeApp } from 'firebase/app';
import firebase from 'firebase/app';
import { getStorage } from "firebase/storage";
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


const firebaseApp = initializeApp(firebaseConfig);

  // Get a reference to the storage service, which is used to create references in your storage bucket
const storage = getStorage(firebaseApp);

export {storage, firebase as default}