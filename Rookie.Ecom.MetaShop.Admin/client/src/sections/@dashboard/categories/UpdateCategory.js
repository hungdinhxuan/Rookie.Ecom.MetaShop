import React, { useState, useEffect } from "react";
import TextField from "@mui/material/TextField";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import Button from "@mui/material/Button";
import { useSelector, useDispatch } from "react-redux";
import { swalWithBootstrapButtons } from "src/utils/sweetalert2";
import { storage } from "src/utils/firebase";
import {
  ref,
  uploadBytesResumable,
  getDownloadURL,
  deleteObject,
} from "firebase/storage";
import uuid from "src/utils/uuid";
import { setCategory, updateCategoryAsync } from "src/features/categorySlice";
import exactFirebaseLink from "src/utils/exactFirebaseLink";

const UpdateCategory = ({ open, setOpen }) => {
  const types = ["image/png", "image/jpeg", "image/jpg"];
  const category = useSelector((state) => state.category.category);
  const dispatch = useDispatch();
  const [previewUrl, setPreviewUrl] = useState(category?.imageUrl);

  const [file, setFile] = useState(null);

  const handleFileChange = (e) => {
    const file = e.target.files[0];

    if (file && types.includes(file.type)) {
      setFile(file);

      const reader = new FileReader();
      reader.onloadend = () => {
        setPreviewUrl(reader.result);
      };
      reader.readAsDataURL(file);
    } else {
      setOpen(false);
      swalWithBootstrapButtons.fire(
        "Error",
        "Please select an image file (png, jpg, jpeg)",
        "error"
      );
    }
  };

  const handleCategoryChange = (e) => {
    dispatch(setCategory({ ...category, [e.target.name]: e.target.value }));
  };

  const handleClose = () => {
    setOpen(false);
    setFile(null);
  };

  const handleUpdate = () => {
    if (file) {
      const storageRef = ref(storage, uuid() + file.name);
      const uploadTask = uploadBytesResumable(storageRef, file);
      uploadTask.on(
        "state_changed",
        (snapshot) => {
          // Observe state change events such as progress, pause, and resume
          // Get task progress, including the number of bytes uploaded and the total number of bytes to be uploaded
          const progress =
            (snapshot.bytesTransferred / snapshot.totalBytes) * 100;
        },
        (error) => {
          // Handle unsuccessful uploads
          console.log(error);
          swalWithBootstrapButtons.fire(
            "Error",
            error || "Something went wrong",
            "error"
          );
        },
        () => {
          // Handle successful uploads on complete
          // For instance, get the download URL: https://firebasestorage.googleapis.com/...
          getDownloadURL(uploadTask.snapshot.ref).then((downloadURL) => {
            console.log("File available at", downloadURL);
            const beforeUrlLink = exactFirebaseLink(category?.imageUrl);

            console.log("beforeUrlLink", category?.imageUrl);

            if (beforeUrlLink !== null) {
              const desertRef = ref(storage, beforeUrlLink);
              deleteObject(desertRef)
                .then(() => {
                  // File deleted successfully
                  console.log("File deleted successfully");
                })
                .catch((error) => {
                  // Uh-oh, an error occurred!
                  console.log(error);
                });
            }
            dispatch(
              updateCategoryAsync({
                id: category.id,
                name: category.name,
                desc: category.desc,
                imageUrl: downloadURL,
              })
            );

            dispatch(setCategory({ ...category, imageUrl: downloadURL }));
          });
        }
      );
    } else {
      if (category.name && category.desc) {
        dispatch(
          updateCategoryAsync({
            id: category.id,
            name: category.name,
            desc: category.desc,
            imageUrl: category.imageUrl,
          })
        );
      } else {
        swalWithBootstrapButtons.fire(
          "Error",
          "Please fill all the fields",
          "error"
        );
      }
    }
    handleClose();
  };
  // console.log(previewUrl);
  return (
    <Dialog open={open} onClose={handleClose}>
      <DialogTitle>Category</DialogTitle>
      <DialogContent>
        <DialogContentText>Create a new category</DialogContentText>
        <TextField
          autoFocus
          margin="dense"
          id="name"
          label="Name"
          type="text"
          fullWidth
          variant="outlined"
          onChange={handleCategoryChange}
          value={category?.name}
          name="name"
        />
        <TextField
          autoFocus
          margin="dense"
          id="description"
          label="Description"
          type="text"
          fullWidth
          variant="outlined"
          value={category?.desc}
          onChange={handleCategoryChange}
          name="desc"
        />
      </DialogContent>
      <input type="file" onChange={handleFileChange} />
      <img src={previewUrl} alt="preview" width={100} height={100} />
      <DialogActions>
        <Button onClick={handleClose}>Cancel</Button>
        <Button onClick={handleUpdate}>Update</Button>
      </DialogActions>
    </Dialog>
  );
};

export default UpdateCategory;
