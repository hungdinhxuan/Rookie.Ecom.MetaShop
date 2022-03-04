import TextField from "@mui/material/TextField";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogTitle from "@mui/material/DialogTitle";
import Button from "@mui/material/Button";
import { useState, forwardRef, useEffect } from "react";
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

const ProductDetail = ({ open, setOpen }) => {
  const dispatch = useDispatch();
  const { categories } = useSelector((state) => state.category);
  const product = useSelector((state) => state.product.product);

  const [previewFiles, setPreviewFiles] = useState(
    product.productPictures["$values"]
  );

  const handleClose = () => {
    setOpen(false);
    setPreviewFiles([]);
  };

  useEffect(() => {
    dispatch(getAllCategoriesAsync());
  }, [dispatch]);

  return (
    <Dialog open={open} onClose={handleClose}>
      <DialogTitle>Product Detail</DialogTitle>
      <DialogContent>
        <TextField
          autoFocus
          margin="dense"
          id="name"
          label="Name"
          type="text"
          fullWidth
          variant="outlined"
          name="name"
          value={product.name}
          readOnly
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
          name="shortDesc"
          readOnly
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
          name="longDesc"
          multiline
          readOnly
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
            name="price"
            id="formatted-price-input"
            InputProps={{
              inputComponent: NumberFormatCustom,
            }}
            variant="standard"
            readOnly
          />

          <TextField
            label="Quantity"
            value={product.quantity}
            name="quantity"
            id="formatted-quantity-input"
            InputProps={{
              inputComponent: NumberFormatCustom2,
            }}
            variant="standard"
            readOnly
          />
        </Box>
        <FormControlLabel
          control={
            <Switch
              checked={product.isPublished}
              readOnly
              inputProps={{ "aria-label": "controlled" }}
            />
          }
          label="Published"
        />

        <Form.Select
          aria-label="Default select example"
          name="categoryId"
          value={product.categoryId}
          readOnly
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
            {previewFiles.map((previewFile, index) => (
              <img
                src={previewFile.pictureUrl || "/static/none.png"}
                alt="preview"
                style={{ width: "100px", height: "100px", objectFit: "cover" }}
                key={index}
              />
            ))}
          </Stack>
        </div>
      </DialogContent>

      <DialogActions>
        <Button onClick={handleClose}>Close</Button>
      </DialogActions>
    </Dialog>
  );
};

export default ProductDetail;
