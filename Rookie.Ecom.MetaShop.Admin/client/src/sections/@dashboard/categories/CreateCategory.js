import TextField from "@mui/material/TextField";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import Button from "@mui/material/Button";
import { useState } from "react";
import { swalWithBootstrapButtons } from "../../../utils/sweetalert2";
import { storage } from "src/utils/firebase";
import { ref, uploadBytesResumable, getDownloadURL } from "firebase/storage";
import { createCategoryAsync } from "src/features/categorySlice";
import { useDispatch } from "react-redux";
import uuid from "src/utils/uuid";

const CreateCategory = ({ open, setOpen }) => {
  const dispatch = useDispatch();
  const types = ["image/png", "image/jpeg", "image/jpg"];
  const [category, setCategory] = useState({
    name: "",
    desc: "",
    imageUrl: "",
  });
  const handleCategoryChange = (e) => {
    setCategory({ ...category, [e.target.name]: e.target.value });
  };
  const [file, setFile] = useState(null);
  const [previewUrl, setPreviewUrl] = useState("");

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

  const handleClose = () => {
    setOpen(false);
    setCategory({
      name: "",
      desc: "",
      imageUrl: "",
    });
    setFile(null);
    setPreviewUrl("");
  };

  const handleCreate = () => {
    // firebase upload
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

            dispatch(
              createCategoryAsync({ ...category, imageUrl: downloadURL })
            );
          });
        }
      );
    } else {
      if (category.name && category.desc) {
        dispatch(createCategoryAsync({ ...category, imageUrl: "" }));
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
          onChange={handleCategoryChange}
          name="desc"
        />
      </DialogContent>
      <input type="file" onChange={handleFileChange} />
      <img src={previewUrl || "/static/none.png"} alt="preview" width={100} height={100} />
      <DialogActions>
        <Button onClick={handleClose}>Cancel</Button>
        <Button onClick={handleCreate}>Create</Button>
      </DialogActions>
    </Dialog>
  );
};

export default CreateCategory;
