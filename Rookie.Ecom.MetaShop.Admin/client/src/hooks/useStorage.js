import React from "react";
import { useState, useEffect } from "react";
import { storage } from "../utils/firebase";

const useStorage = (file) => {
  const [progess, setProgess] = useState(0);
  const [url, setUrl] = useState(null);
  const [error, setError] = useState(null);

  useEffect(() => {
    const storageRef = storage.ref(file.name);
    storageRef.put(file).on(
      "state_changed",
      (snapshot) => {
        let percentage =
          (snapshot.bytesTransferred / snapshot.totalBytes) * 100;
        setProgess(percentage);
      },
      (error) => {
        setError(error);
      },
      () => {
        storageRef.getDownloadURL().then((url) => {
          setUrl(url);
        });
      }
    );
  }, [file]);
  return { progess, url, error };
};

export default useStorage;
