import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axiosClient from "../utils/axiosClient";
import { swalWithBootstrapButtons } from "../utils/sweetalert2";
import { ref, deleteObject } from "firebase/storage";
import {storage} from "../utils/firebase";
import {exactFirebaseLink} from "../utils/firebase";
import {LIMIT_PRODUCT_PER_PAGE} from "../app/constants";

const initialState = {
  products: [],
  product: null,
  currentPage: 1,
  totalPages: 1,
  totalItems: 0,
  loading: false
};

export const getAllProductsAsync = createAsyncThunk(
  "products/getAllproducts",
  async (values, { rejectWithValue }) => {
    try {
      const response = await axiosClient.get(`/product/find${values}`);
      return response;
    } catch (error) {
      return rejectWithValue(error.response);
    }
  }
);

export const deleteProductAsync = createAsyncThunk(
  "products/deleteProduct",
  async (values, { rejectWithValue }) => {
    try {
      const response = await axiosClient.delete(`/product/${values.id}`);
      return response;
    } catch (error) {
      return rejectWithValue(error.response);
    }
  }
);

export const createProductAsync = createAsyncThunk(
  "products/createProduct",
  async (values, { rejectWithValue }) => {
    try {
      const response = await axiosClient.post(`/product`, values);
      return response;
    } catch (error) {
      return rejectWithValue(error.response);
    }
  }
);

export const updateProductAsync = createAsyncThunk(
  "products/updateProduct",
  async (values, { rejectWithValue }) => {
    try {
      const response = await axiosClient.put(`/product`, values);
      return response;
    } catch (error) {
      return rejectWithValue(error.response);
    }
  }
);

export const productSlice = createSlice({
  name: "product",
  initialState,
  reducers: {
    setProduct: (state, action) => {
      state.product = action.payload;
    },
    setCurrentPage: (state, action) => {
      state.currentPage = action.payload;
    },
    
  },
  extraReducers: (builder) => {
    builder
      .addCase(getAllProductsAsync.pending, (state, action) => {
          state.loading = true
      })
      .addCase(getAllProductsAsync.fulfilled, (state, action) => {
        console.log(action.payload);
        state.products = action.payload.items["$values"];
        state.totalPages = action.payload.totalPages;
        state.currentPage = action.payload.currentPage;
        state.totalItems = action.payload.totalItems;
        state.loading = false
      })
      .addCase(getAllProductsAsync.rejected, (state, action) => {
        state.loading = false
    })
      .addCase(deleteProductAsync.fulfilled, (state, action) => {
        console.log(state.product);
        state.products = state.products.filter(
          (product) => product.id !== state.product.id
        );
        
        swalWithBootstrapButtons.fire(
          "Deleted!",
          "Delete successfully",
          "success"
        );
        
        const deletePromise = [];
        state.product.productPictures["$values"].forEach((productPic) => {
          const beforeUrlLink = exactFirebaseLink(productPic.pictureUrl);
          if (beforeUrlLink !== null) {
            const desertRef = ref(storage, beforeUrlLink);
            deletePromise.push(deleteObject(desertRef));
          }
        });

        
        Promise.all(deletePromise).then(() => {
          console.log("All files deleted");
        }).catch((error) => {
          console.log(error);
        });

        state.product = null;

        state.totalItems = state.totalItems - 1;
        if(state.totalItems % LIMIT_PRODUCT_PER_PAGE === 0){
          state.totalPages = state.totalPages - 1;

          
          state.currentPage = state.currentPage - 1;
        }
        
      })
      .addCase(deleteProductAsync.rejected, (state, action) => {
        swalWithBootstrapButtons.fire(
          "Error",
          action.error || "Something went wrong.",
          "error"
        );
        state.product = null;
      })
      .addCase(createProductAsync.fulfilled, (state, action) => {
        state.products.push(action.payload);
        state.totalItems = state.totalItems + 1;

        if(state.totalItems / LIMIT_PRODUCT_PER_PAGE > state.totalPages){
          state.totalPages = state.totalPages + 1;
        }
        swalWithBootstrapButtons.fire(
          "Created!",
          "A new product has been created.",
          "success"
        );
      })
      .addCase(createProductAsync.rejected, (state, action) => {
        swalWithBootstrapButtons.fire(
          "Error",
          action.error || "Something went wrong.",
          "error"
        );
      })
      
      .addCase(updateProductAsync.fulfilled, (state, action) => {
        const index = state.products.findIndex(
          (product) => product.id === state.product.id
        );

        
        state.products[index] = state.product;

        swalWithBootstrapButtons.fire(
          "Updated!",
          "A product has been updated.",
          "success"
        );

      })
      .addCase(updateProductAsync.rejected, (state, action) => {
        swalWithBootstrapButtons.fire(
          "Error",
          action.error || "Something went wrong.",
          "error"
        );
      });
  },
});

export const { setProduct, setCurrentPage } = productSlice.actions;
export default productSlice.reducer;
