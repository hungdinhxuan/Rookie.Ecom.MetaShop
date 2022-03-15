import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axiosClient from "../utils/axiosClient";


const initialState = {
  users: [],
  currentPage: 1,
  totalPages: 1,
  totalItems: 0,
  loading: false
};

export const getAllUsersAsync = createAsyncThunk(
  "users/getAllUsers",
  async (values, { rejectWithValue }) => {
    try {
      const response = await axiosClient.get(`/user/find${values}`);
      return response;
    } catch (error) {
      return rejectWithValue(error.response);
    }
  }
);


export const userSlice = createSlice({
  name: "user",
  initialState,
  extraReducers: (builder) => {
    builder
      .addCase(getAllUsersAsync.pending, (state, action) => {
          state.loading = true
      })
      .addCase(getAllUsersAsync.fulfilled, (state, action) => {
        console.log(action.payload);
        state.users = action.payload.items["$values"];
        state.totalPages = action.payload.totalPages;
        state.currentPage = action.payload.currentPage;
        state.totalItems = action.payload.totalItems;
        state.loading = false
      })
      .addCase(getAllUsersAsync.rejected, (state, action) => {
        state.loading = false
    })
  },
});

export default userSlice.reducer;
