// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import {getStorage} from "firebase/storage";
import { ref, uploadBytesResumable, getDownloadURL } from "firebase/storage";
import uuid from "./uuid";

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


export async function uploadImageToFirebaseAsPromise (imageFile) {
  return new Promise(function (resolve, reject) {
    const storageRef = ref(storage, uuid() + imageFile.name);
    const uploadTask = uploadBytesResumable(storageRef, imageFile);
    uploadTask.on(
      "state_changed",
      (snapshot) => {
        
      },
      (error) => {

        reject(error);
      },
      () => {
        // Handle successful uploads on complete
        // For instance, get the download URL: https://firebasestorage.googleapis.com/...
        getDownloadURL(uploadTask.snapshot.ref).then((downloadURL) => {
          console.log("File available at", downloadURL);
          resolve(downloadURL);
        });
      }
    );
  });
}

export const exactFirebaseLink = (link) => {
  try {
      return link.match(/o\/.*\?/g)[0].split('o/')[1].split('?')[0];
  } catch (error) {
      return null;
  }
}