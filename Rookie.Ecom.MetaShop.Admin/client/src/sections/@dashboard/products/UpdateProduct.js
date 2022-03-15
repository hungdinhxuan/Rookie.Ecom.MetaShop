import TextField from "@mui/material/TextField";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import Button from "@mui/material/Button";
import { useState, forwardRef, useEffect } from "react";
import { swalWithBootstrapButtons } from "../../../utils/sweetalert2";
import { exactFirebaseLink } from "src/utils/firebase";
import { updateProductAsync, setProduct } from "src/features/productSlice";
import { getAllCategoriesAsync } from "src/features/categorySlice";
import { useDispatch, useSelector } from "react-redux";
import { styled } from "@mui/material/styles";
import Stack from "@mui/material/Stack";
import { Form } from "react-bootstrap";

import PropTypes from "prop-types";
import NumberFormat from "react-number-format";
import Box from "@mui/material/Box";
import Switch from "@mui/material/Switch";
import FormControlLabel from "@mui/material/FormControlLabel";
import { storage, uploadImageToFirebaseAsPromise } from "src/utils/firebase";
import { ref, deleteObject } from "firebase/storage";

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

const NumberFormatCustom2 = forwardRef(function NumberFormatCustom2(
  props,
  ref
) {
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
    />
  );
});

NumberFormatCustom.propTypes = {
  name: PropTypes.string.isRequired,
  onChange: PropTypes.func.isRequired,
};

NumberFormatCustom2.propTypes = {
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

const UpdateProduct = ({ open, setOpen }) => {
  const dispatch = useDispatch();
  const types = ["image/png", "image/jpeg", "image/jpg"];
  const { categories } = useSelector((state) => state.category);
  const product = useSelector((state) => state.product.product);
  const handleProductChange = (e) => {
    dispatch(setProduct({ ...product, [e.target.name]: e.target.value }));
  };

  const [selectedFiles, setSelectedFiles] = useState([]);
  const [previewFiles, setPreviewFiles] = useState(
    product.productPictures["$values"].map((item) => item.pictureUrl)
  );

  const handleSelectedFilesChange = (e) => {
    const files = convertFileListToArray(e.target.files);

    if (files.length > 3) {
      handleClose();
      swalWithBootstrapButtons.fire({
        title: "Error",
        text: "You can only upload maximum 3 images",
        icon: "error",
        confirmButtonText: "Ok",
      });
      return;
    }
    if (files.map((file) => file.type).some((type) => !types.includes(type))) {
      handleClose();
      swalWithBootstrapButtons.fire({
        title: "Error",
        text: "You can only upload png, jpg, jpeg images",
        icon: "error",
        confirmButtonText: "Ok",
      });
      return;
    }

    if(files.length > 0)
    {
      setPreviewFiles(files.map((file) => URL.createObjectURL(file)));
      setSelectedFiles(files);
    }
    
    // files.forEach((file) => {
    //   const reader = new FileReader();
    //   reader.onload = () => {
    //     setPreviewFiles((previewFiles) => [...previewFiles, reader.result]);
    //   };
    //   reader.readAsDataURL(file);
    // });
  };

  const handleClose = () => {
    setOpen(false);
    setSelectedFiles([]);
    setPreviewFiles([]);
  };

  console.log(previewFiles);

  const handleUpdateProduct = () => {
    // Simple validation
    if (
      !product.name ||
      !product.shortDesc ||
      !product.price ||
      !product.quantity ||
      !product.categoryId
    ) {
      handleClose();
      swalWithBootstrapButtons.fire({
        title: "Error",
        text: "You must fill all the fields",
        icon: "error",
        confirmButtonText: "Ok",
      });
      return;
    } 

    // if upload new pictures
    if (selectedFiles.length > 0) {
      const downloadURLs = [];
      selectedFiles.forEach((file) => {
        downloadURLs.push(uploadImageToFirebaseAsPromise(file));
      });
      handleClose();

      // update product picture
      Promise.all(downloadURLs).then((urls) => {
        let newProduct = {
          id: product.id,
          name: product.name,
          shortDesc: product.shortDesc,
          longDesc: product.longDesc,
          price: product.price,
          quantity: product.quantity,
          isFeatured: product.isFeatured,
          categoryId: product.categoryId,
          newProductPictureDtos: urls.map((url) => ({
            pictureUrl: url,
            productId: product.id
          })),
          productPictureDtos: product.productPictures["$values"]
          
        }
        dispatch(setProduct({
          ...newProduct,
          productPictures: {
            "$values": newProduct.newProductPictureDtos
          }
        }));

        dispatch(
          updateProductAsync(newProduct)
        );

      
      });

      // remove old pictures
      const deletePromise = [];
      product.productPictures["$values"].forEach((productPictureDto) => {
        const beforeUrlLink = exactFirebaseLink(productPictureDto.pictureUrl);

        if (beforeUrlLink !== null) {
          const desertRef = ref(storage, beforeUrlLink);
          deletePromise.push(deleteObject(desertRef));
        }
      });

      Promise.all(deletePromise)
        .then(() => {
          console.log("deleted");
        })
        .catch((error) => {
          console.log(error);
        });

        // revoke all urls
        previewFiles.forEach((url) => URL.revokeObjectURL(url));

    } else {
      if (
        product.name &&
        product.shortDesc &&
        product.price &&
        product.quantity &&
        product.categoryId
      ) {
        let newProduct = {
          id: product.id,
          name: product.name,
          shortDesc: product.shortDesc,
          longDesc: product.longDesc,
          price: product.price,
          quantity: product.quantity,
          isFeatured: product.isFeatured,
          categoryId: product.categoryId,
          productPictureDtos: product.productPictures["$values"],
        }
        dispatch(setProduct({
          ...newProduct,
          productPictureDtos: {
            "$values": product.productPictures["$values"]
          }
        }));
        dispatch(
          updateProductAsync(newProduct)
        );
      } else {
        swalWithBootstrapButtons.fire({
          title: "Error",
          text: "You must fill all the fields",
          icon: "error",
          confirmButtonText: "Ok",
        });
      }
      handleClose();
    }
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
          value={product.name}
        />
        <TextField
          autoFocus
          margin="dense"
          id="shortDesc"
          label="Short Description"
          value={product.shortDesc}
          type="text"
          fullWidth
          variant="outlined"
          onChange={handleProductChange}
          name="shortDesc"
        />
        <TextField
          autoFocus
          margin="dense"
          id="longDesc"
          label="Long Description"
          type="text"
          fullWidth
          variant="outlined"
          value={product.longDesc}
          onChange={handleProductChange}
          name="longDesc"
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
              inputComponent: NumberFormatCustom2,
            }}
            variant="standard"
          />
        </Box>
        <FormControlLabel
          control={
            <Switch
              checked={product.isFeatured}
              onChange={(e) => {
                dispatch(
                  setProduct({ ...product, isFeatured: e.target.checked })
                );
              }}
              inputProps={{ "aria-label": "controlled" }}
            />
          }
          label="Featured Product"
        />

        <Form.Select
          aria-label="Default select example"
          name="categoryId"
          onChange={handleProductChange}
          value={product.categoryId}
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

          {
            <Stack direction="row" alignItems="center" spacing={2}>
              {previewFiles.map((previewFile, index) => (
                <img
                  src={previewFile || "/static/none.png"}
                  alt="preview"
                  style={{
                    width: "100px",
                    height: "100px",
                    objectFit: "cover",
                  }}
                  key={index}
                />
              ))}
            </Stack>
          }
        </div>
      </DialogContent>

      <DialogActions>
        <Button onClick={handleClose}>Cancel</Button>
        <Button onClick={handleUpdateProduct}>Update</Button>
      </DialogActions>
    </Dialog>
  );
};

export default UpdateProduct;
