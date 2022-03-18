import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axiosClient from "../utils/axiosClient";


const initialState = {
  overview:null,
  loading: false
};

export const getOverviewAsync = createAsyncThunk(
  "overview/Statistics",
  async (values, { rejectWithValue }) => {
    try {
      const response = await axiosClient.get(`/overview`);
      return response;
    } catch (error) {
      return rejectWithValue(error.response);
    }
  }
);


export const overviewSlice = createSlice({
  name: "overview",
  initialState,
  extraReducers: (builder) => {
    builder
      .addCase(getOverviewAsync.pending, (state, action) => {
          state.loading = true
      })
      .addCase(getOverviewAsync.fulfilled, (state, action) => {
        console.log(action.payload);
        state.overview = action.payload
      })
      .addCase(getOverviewAsync.rejected, (state, action) => {
        state.loading = false
    })
  },
});

export default overviewSlice.reducer;
