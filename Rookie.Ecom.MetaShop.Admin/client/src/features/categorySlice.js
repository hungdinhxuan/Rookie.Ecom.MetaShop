import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axiosClient from "../utils/axiosClient";
import { swalWithBootstrapButtons } from "../utils/sweetalert2";
import { ref, deleteObject } from "firebase/storage";
import {storage} from "../utils/firebase";
import exactFirebaseLink from "../utils/exactFirebaseLink";

const initialState = {
  categories: [],
  category: null,
};

export const getAllCategoriesAsync = createAsyncThunk(
  "categories/getAllCategories",
  async (values, { rejectWithValue }) => {
    try {
      const response = await axiosClient.get(`/category`);
      return response;
    } catch (error) {
      return rejectWithValue(error.response.data);
    }
  }
);

export const deleteCategoryAsync = createAsyncThunk(
  "categories/deleteCategory",
  async (values, { rejectWithValue }) => {
    try {
      const response = await axiosClient.delete(`/category/${values.id}`);
      return response;
    } catch (error) {
      return rejectWithValue(error.response.data);
    }
  }
);

export const createCategoryAsync = createAsyncThunk(
  "categories/createCategory",
  async (values, { rejectWithValue }) => {
    try {
      const response = await axiosClient.post(`/category`, values);
      return response;
    } catch (error) {
      return rejectWithValue(error.response.data);
    }
  }
);

export const updateCategoryAsync = createAsyncThunk(
  "categories/updateCategory",
  async (values, { rejectWithValue }) => {
    try {
      const response = await axiosClient.put(`/category`, values);
      return response;
    } catch (error) {
      return rejectWithValue(error.response.data);
    }
  }
);

export const categorySlice = createSlice({
  name: "category",
  initialState,
  reducers: {
    setCategory: (state, action) => {
      state.category = action.payload;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(getAllCategoriesAsync.fulfilled, (state, action) => {
        state.categories = action.payload.data;
      })
      .addCase(deleteCategoryAsync.fulfilled, (state, action) => {
        console.log(state.category);
        state.categories = state.categories.filter(
          (category) => category.id !== state.category.id
        );
        
        swalWithBootstrapButtons.fire(
          "Deleted!",
          "Delete successfully",
          "success"
        );

        const objLink = exactFirebaseLink(state.category.imageUrl);

        if(objLink){

          const desertRef = ref(storage, objLink);
          deleteObject(desertRef).then(() => {
            // File deleted successfully
            console.log("File deleted successfully");
          }).catch((error) => {
            // Uh-oh, an error occurred!
            console.log(error);
          });
        }
        state.category = null;
      })
      .addCase(deleteCategoryAsync.rejected, (state, action) => {
        swalWithBootstrapButtons.fire(
          "Error",
          action.error || "Something went wrong.",
          "error"
        );
        state.category = null;
      })
      .addCase(createCategoryAsync.fulfilled, (state, action) => {
        state.categories.push(action.payload.data);

        swalWithBootstrapButtons.fire(
          "Created!",
          "A new category has been created.",
          "success"
        );
      })
      .addCase(createCategoryAsync.rejected, (state, action) => {
        swalWithBootstrapButtons.fire(
          "Error",
          action.error || "Something went wrong.",
          "error"
        );
      })
      
      .addCase(updateCategoryAsync.fulfilled, (state, action) => {
        const index = state.categories.findIndex(
          (category) => category.id === state.category.id
        );
        state.categories[index] = state.category;

        swalWithBootstrapButtons.fire(
          "Updated!",
          "A category has been updated.",
          "success"
        );

      })
      .addCase(updateCategoryAsync.rejected, (state, action) => {
        swalWithBootstrapButtons.fire(
          "Error",
          action.error || "Something went wrong.",
          "error"
        );
      });
  },
});

export const { setCategory } = categorySlice.actions;
export default categorySlice.reducer;
