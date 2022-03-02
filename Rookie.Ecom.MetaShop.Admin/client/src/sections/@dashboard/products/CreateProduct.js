import TextField from "@mui/material/TextField";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import Button from "@mui/material/Button";
import { useState, forwardRef, useEffect } from "react";
import { swalWithBootstrapButtons } from "../../../utils/sweetalert2";
import { storage } from "src/utils/firebase";
import { ref, uploadBytesResumable, getDownloadURL } from "firebase/storage";
import { createProductAsync } from "src/features/productSlice";
import {getAllCategoriesAsync} from "src/features/categorySlice";
import { useDispatch, useSelector } from "react-redux";
import uuid from "src/utils/uuid";
import { styled } from "@mui/material/styles";
import Stack from "@mui/material/Stack";
import { Form } from "react-bootstrap";

import PropTypes from "prop-types";
import NumberFormat from "react-number-format";
import Box from "@mui/material/Box";
import Switch from '@mui/material/Switch';
import FormControlLabel from '@mui/material/FormControlLabel';

const NumberFormatCustom = forwardRef(function NumberFormatCustom(props, ref) {
  const { onChange, ...other } = props;

  return (
    <NumberFormat
      {...other}
      getInputRef={ref}
      onValueChange={(values) => {
        onChange({
          target: {
            name: props.name,
            value: values.value,
          },
        });
      }}
      thousandSeparator
      isNumericString
      prefix="$"
    />
  );
});

NumberFormatCustom.propTypes = {
  name: PropTypes.string.isRequired,
  onChange: PropTypes.func.isRequired,
};

const Input = styled("input")({
  display: "none",
});

const convertFileListToArray = (fileList) => {
  const array = [];
  for (let i = 0; i < fileList.length; i++) {
    array.push(fileList.item(i));
  }
  return array;
};

const CreateProduct = ({ open, setOpen }) => {
  const dispatch = useDispatch();
  const types = ["image/png", "image/jpeg", "image/jpg"];
  const { categories } = useSelector((state) => state.category);
  const [product, setProduct] = useState({
    name: "",
    desc: "",
    imageUrl: "",
    shortDesc: "",
    longDesc: "",
    price: 0,
    quantity: 0,
    isPublished: true,
    categoryId: "",
  });
  const handleProductChange = (e) => {
    setProduct({ ...product, [e.target.name]: e.target.value });
  };
  const [file, setFile] = useState(null);
  const [previewUrl, setPreviewUrl] = useState("");

  const [selectedFiles, setSelectedFiles] = useState([]);
  const [previewFiles, setPreviewFiles] = useState([]);

  const onSelectCategory = (value) => {
    console.log(value);
    setProduct({ ...product, categoryId: value });
  };

  function onSearchCategory(val) {
    console.log("search:", val);
  }

  const handleSelectedFilesChange = (e) => {
    const files = convertFileListToArray(e.target.files);

    if (files.length > 3) {
      swalWithBootstrapButtons.fire({
        title: "Error",
        text: "You can only upload maximum 3 images",
        icon: "error",
        confirmButtonText: "Ok",
      });
      return;
    }
    if (files.map((file) => file.type).some((type) => !types.includes(type))) {
      swalWithBootstrapButtons.fire({
        title: "Error",
        text: "You can only upload png, jpg, jpeg images",
        icon: "error",
        confirmButtonText: "Ok",
      });
      return;
    }
    files.forEach((file) => {
      const reader = new FileReader();
      reader.onload = () => {
        setPreviewFiles((previewFiles) => [...previewFiles, reader.result]);
      };
      reader.readAsDataURL(file);
    });
    setSelectedFiles(files);
  };

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
    setProduct({
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

            dispatch(createProductAsync({ ...product, imageUrl: downloadURL }));
          });
        }
      );
    } else {
      if (product.name && product.desc) {
        dispatch(createProductAsync({ ...product, imageUrl: "" }));
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

  useEffect(() => {
    dispatch(getAllCategoriesAsync());
  }, [dispatch]);

  return (
    <Dialog open={open} onClose={handleClose}>
      <DialogTitle>Product</DialogTitle>
      <DialogContent>
        <DialogContentText>Create a new product</DialogContentText>
        <TextField
          autoFocus
          margin="dense"
          id="name"
          label="Name"
          type="text"
          fullWidth
          variant="outlined"
          onChange={handleProductChange}
          name="name"
        />
        <TextField
          autoFocus
          margin="dense"
          id="shortDesc"
          label="Short Description"
          type="text"
          fullWidth
          variant="outlined"
          onChange={handleProductChange}
          name="desc"
        />
        <TextField
          autoFocus
          margin="dense"
          id="longDesc"
          label="Long Description"
          type="text"
          fullWidth
          variant="outlined"
          onChange={handleProductChange}
          name="desc"
          multiline
        />

        <Box
          sx={{
            "& > :not(style)": {
              m: 1,
            },
          }}
        >
          <TextField
            label="Price"
            value={product.price}
            onChange={handleProductChange}
            name="price"
            id="formatted-price-input"
            InputProps={{
              inputComponent: NumberFormatCustom,
            }}
            variant="standard"
          />

          <TextField
            label="Quantity"
            value={product.quantity}
            onChange={handleProductChange}
            name="quantity"
            id="formatted-quantity-input"
            InputProps={{
              inputComponent: NumberFormatCustom,
            }}
            variant="standard"
          />
        </Box>
        <FormControlLabel control={
           <Switch
         
           checked={product.isPublished}
           onChange={(e) => {
             setProduct({ ...product, isPublished: e.target.checked });
           }}
           inputProps={{ 'aria-label': 'controlled' }}
         />
        } label="Published" />
       
        <Form.Select
          aria-label="Default select example"
          name="categoryId"
          onChange={handleProductChange}
        >
          <option value="">Select a category</option>
          {categories &&
            categories.map((category) => (
              <option key={category.id} value={category.id}>
                {category.name}
              </option>
            ))}
        </Form.Select>
        <div
          style={{
            margin: "auto",
            display: "flex",
            justifyContent: "space-between",
            height: "150px",
            flexDirection: "column",
            marginTop: "20px",
          }}
        >
          <Stack direction="row" alignItems="center" spacing={2}>
            <label htmlFor="contained-button-file">
              <Input
                accept="image/*"
                id="contained-button-file"
                multiple
                type="file"
                onChange={handleSelectedFilesChange}
              />
              <Button variant="contained" component="span">
                Upload Photo
              </Button>
            </label>
          </Stack>

          {selectedFiles.length > 0 && (
            <Stack direction="row" alignItems="center" spacing={2}>
              {previewFiles.map((previewFile, index) => (
                <img
                  src={previewFile || "/static/none.png"}
                  alt="preview"
                  style={{ width: "100px", height: "100px" }}
                  key={index}
                />
              ))}
            </Stack>
          )}
        </div>
      </DialogContent>

      <DialogActions>
        <Button onClick={handleClose}>Cancel</Button>
        <Button onClick={handleCreate}>Create</Button>
      </DialogActions>
    </Dialog>
  );
};

export default CreateProduct;
