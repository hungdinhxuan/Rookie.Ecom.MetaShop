import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axiosClient from "../utils/axiosClient";
import {swalWithBootstrapButtons} from "../utils/sweetalert2";

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

export const categorySlice = createSlice({
  name: "category",
  initialState,
  reducers: {
    setCategory: (state, action) => {
      state.category = action.payload;
    }
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
        state.category = null;
        swalWithBootstrapButtons.fire(
          'Deleted!',
          'Your file has been deleted.',
          'success'
        )
      }).addCase(deleteCategoryAsync.rejected, (state, action) => {
        swalWithBootstrapButtons.fire(
          'Error',
          action.error || 'Something went wrong.',
          'error'
        )
        state.category = null;
      });
  },
});

export const { setCategory } = categorySlice.actions;
export default categorySlice.reducer;
