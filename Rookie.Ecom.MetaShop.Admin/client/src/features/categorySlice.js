import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axiosClient from "../utils/axiosClient";
import { swalWithBootstrapButtons } from "../utils/sweetalert2";
import { ref, deleteObject } from "firebase/storage";
import {storage} from "../utils/firebase";
import exactFirebaseLink from "../utils/exactFirebaseLink";
import {LIMIT_CATEGORY_PER_PAGE} from "../app/constants";

const initialState = {
  categories: [],
  category: null,
  currentPage: 1,
  totalPages: 1,
  totalItems: 0,
};

export const getAllCategoriesAsync = createAsyncThunk(
  "categories/getAllCategories",
  async (values, { rejectWithValue }) => {
    try {
      const response = await axiosClient.get(`/category/find${values}`);
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
    setCurrentPage: (state, action) => {
      state.currentPage = action.payload;
    },
    
  },
  extraReducers: (builder) => {
    builder
      .addCase(getAllCategoriesAsync.fulfilled, (state, action) => {
        console.log(action.payload.data);
        state.categories = action.payload.data.items;
        state.totalPages = action.payload.data.totalPages;
        state.currentPage = action.payload.data.currentPage;
        state.totalItems = action.payload.data.totalItems;
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

        state.totalItems = state.totalItems - 1;
        if(state.totalItems % LIMIT_CATEGORY_PER_PAGE === 0){
          state.totalPages = state.totalPages - 1;

          
          state.currentPage = state.currentPage - 1;
        }
        
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
        state.totalItems = state.totalItems + 1;

        if(state.totalItems / LIMIT_CATEGORY_PER_PAGE > state.totalPages){
          state.totalPages = state.totalPages + 1;
        }
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

export const { setCategory, setCurrentPage } = categorySlice.actions;
export default categorySlice.reducer;
