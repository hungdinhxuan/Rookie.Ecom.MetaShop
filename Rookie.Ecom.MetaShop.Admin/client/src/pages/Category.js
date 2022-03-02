import { filter } from "lodash";
import { useState } from "react";
import {
  Link as RouterLink,
  useSearchParams,
  useNavigate,
} from "react-router-dom";
// material
import {
  Card,
  Table,
  Stack,
  Button,
  Checkbox,
  TableRow,
  TableBody,
  TableCell,
  Container,
  Typography,
  TableContainer,
  Pagination,
  CircularProgress,
  Box,
} from "@mui/material";
// components
import Page from "../components/Page";
import Scrollbar from "../components/Scrollbar";
import Iconify from "../components/Iconify";
import SearchNotFound from "../components/SearchNotFound";
import {
  CategoryListHead,
  CategoryListToolbar,
  CategoryMoreMenu,
} from "../sections/@dashboard/categories";
// ----------------------------------------------------------------------

import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { getPagedCategoriesAsync } from "../features/categorySlice";

// ----------------------------------------------------------------------
import CreateCategory from "src/sections/@dashboard/categories/CreateCategory";
import parseObjectToUrlQuery from "src/utils/parseObjectToUrlQuery";
import { LIMIT_CATEGORY_PER_PAGE } from "src/app/constants";

const TABLE_HEAD = [
  { id: "id", label: "Id", alignRight: false },
  { id: "name", label: "Name", alignRight: false },
  { id: "desc", label: "Description", alignRight: false },
  { id: "imamgeUrl", label: "Image", alignRight: false },
  { id: "createdDate", label: "Created Date", alignRight: false },
  { id: "updatedDate", label: "Updated Date", alignRight: false },
  { id: "" },
];

// ----------------------------------------------------------------------

function descendingComparator(a, b, orderBy) {
  if (b[orderBy] < a[orderBy]) {
    return -1;
  }
  if (b[orderBy] > a[orderBy]) {
    return 1;
  }
  return 0;
}

function getComparator(order, orderBy) {
  return order === "desc"
    ? (a, b) => descendingComparator(a, b, orderBy)
    : (a, b) => -descendingComparator(a, b, orderBy);
}

function applySortFilter(array, comparator, query) {
  const stabilizedThis = array.map((el, index) => [el, index]);
  stabilizedThis.sort((a, b) => {
    const order = comparator(a[0], b[0]);
    if (order !== 0) return order;
    return a[1] - b[1];
  });
  if (query) {
    return filter(
      array,
      (_Category) =>
        _Category.name.toLowerCase().indexOf(query.toLowerCase()) !== -1
    );
  }
  return stabilizedThis.map((el) => el[0]);
}

export default function Category() {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();
  const {
    categories: CategoryLIST,
    totalPages: CategoryTotalPages,
    currentPage: CategoryCurrentPage,
    loading: CategoryLoading,
  } = useSelector((state) => state.category);

  const dispatch = useDispatch();

  const [open, setOpen] = useState(false);

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const [page, setPage] = useState(0);
  const [order, setOrder] = useState("asc");
  const [selected, setSelected] = useState([]);
  const [orderBy, setOrderBy] = useState("name");
  const [filterName, setFilterName] = useState("");
  const [rowsPerPage, setRowsPerPage] = useState(LIMIT_CATEGORY_PER_PAGE);

  const handleRequestSort = (event, property) => {
    const isAsc = orderBy === property && order === "asc";
    setOrder(isAsc ? "desc" : "asc");
    setOrderBy(property);
  };

  const handleSelectAllClick = (event) => {
    if (event.target.checked) {
      const newSelecteds = CategoryLIST.map((n) => n.name);
      setSelected(newSelecteds);
      return;
    }
    setSelected([]);
  };

  const handleClick = (event, name) => {
    const selectedIndex = selected.indexOf(name);
    let newSelected = [];
    if (selectedIndex === -1) {
      newSelected = newSelected.concat(selected, name);
    } else if (selectedIndex === 0) {
      newSelected = newSelected.concat(selected.slice(1));
    } else if (selectedIndex === selected.length - 1) {
      newSelected = newSelected.concat(selected.slice(0, -1));
    } else if (selectedIndex > 0) {
      newSelected = newSelected.concat(
        selected.slice(0, selectedIndex),
        selected.slice(selectedIndex + 1)
      );
    }
    setSelected(newSelected);
  };

  const handleChangePage = (event, newPage) => {
    navigate({
      pathname: "/dashboard/category",
      search: parseObjectToUrlQuery({
        page: newPage,
        limit: rowsPerPage,
      }),
    });
  };

  const handleFilterByName = (event) => {
    setFilterName(event.target.value);
  };

  const emptyRows =
    page > 0 ? Math.max(0, (1 + page) * rowsPerPage - CategoryLIST.length) : 0;

  const filteredCategorys = applySortFilter(
    CategoryLIST,
    getComparator(order, orderBy),
    filterName
  );

  const isCategoryNotFound = filteredCategorys.length === 0;

  useEffect(() => {
    
    dispatch(
      getPagedCategoriesAsync(
        parseObjectToUrlQuery({
          page: parseInt(searchParams.get("page")) || CategoryCurrentPage,
          limit: parseInt(searchParams.get("limit")) || LIMIT_CATEGORY_PER_PAGE,
        })
      )
    );
  }, [dispatch, searchParams, CategoryCurrentPage]);


  useEffect(() => {
    if (!searchParams.get("page") && !searchParams.get("limit")) {
      navigate({
        pathname: "/dashboard/category",
        search: parseObjectToUrlQuery({
          page: 1,
          limit: LIMIT_CATEGORY_PER_PAGE,
        }),
      });
    }
    console.log('re-render category')
  }, [searchParams, navigate]);

  return (
    <Page title="Category">
      <Container>
        <Stack
          direction="row"
          alignItems="center"
          justifyContent="space-between"
          mb={LIMIT_CATEGORY_PER_PAGE}
        >
          <Typography variant="h4" gutterBottom>
            Category
          </Typography>
          <Button
            variant="contained"
            to="#"
            startIcon={<Iconify icon="eva:plus-fill" />}
            onClick={handleClickOpen}
          >
            New Category
          </Button>
        </Stack>

        <Card>
          <CategoryListToolbar
            numSelected={selected.length}
            filterName={filterName}
            onFilterName={handleFilterByName}
          />

          <Scrollbar>
            <TableContainer sx={{ minWidth: 800 }}>
              <Table>
                <CategoryListHead
                  order={order}
                  orderBy={orderBy}
                  headLabel={TABLE_HEAD}
                  rowCount={CategoryLIST.length}
                  numSelected={selected.length}
                  onRequestSort={handleRequestSort}
                  onSelectAllClick={handleSelectAllClick}
                />
                <TableBody>
                  {CategoryLoading ? (
                    <TableRow>
                      <TableCell>
                        <Box sx={{ display: "flex" }}>
                          <CircularProgress />
                        </Box>
                      </TableCell>
                    </TableRow>
                  ) : (
                    filteredCategorys
                      .slice(
                        page * rowsPerPage,
                        page * rowsPerPage + rowsPerPage
                      )
                      .map((row) => {
                        const {
                          id,
                          name,
                          desc,
                          imageUrl,
                          createdDate,
                          updatedDate,
                        } = row;
                        const isItemSelected = selected.indexOf(name) !== -1;

                        return (
                          <TableRow
                            hover
                            key={id}
                            tabIndex={-1}
                            role="checkbox"
                            selected={isItemSelected}
                            aria-checked={isItemSelected}
                          >
                            <TableCell padding="checkbox">
                              <Checkbox
                                checked={isItemSelected}
                                onChange={(event) => handleClick(event, name)}
                              />
                            </TableCell>
                            <TableCell
                              component="th"
                              scope="row"
                              padding="none"
                            >
                              <Stack
                                direction="row"
                                alignItems="center"
                                spacing={2}
                              >
                                <Typography variant="subtitle2" noWrap>
                                  {id}
                                </Typography>
                              </Stack>
                            </TableCell>
                            <TableCell align="left">{name}</TableCell>
                            <TableCell align="left">{desc}</TableCell>
                            <TableCell align="left">
                              <img
                                src={imageUrl || "/static/none.png"}
                                alt={name}
                                width="80"
                                height="80"
                              />
                            </TableCell>
                            <TableCell align="left">
                              {new Date(createdDate).toLocaleString()}
                            </TableCell>
                            <TableCell align="left">
                              {new Date(updatedDate).toLocaleString()}
                            </TableCell>

                            <TableCell align="right">
                              <CategoryMoreMenu category={row} />
                            </TableCell>
                          </TableRow>
                        );
                      })
                  )}

                  {emptyRows > 0 && (
                    <TableRow style={{ height: 53 * emptyRows }}>
                      <TableCell colSpan={6} />
                    </TableRow>
                  )}
                </TableBody>
                {isCategoryNotFound && (
                  <TableBody>
                    <TableRow>
                      <TableCell align="center" colSpan={6} sx={{ py: 3 }}>
                        <SearchNotFound searchQuery={filterName} />
                      </TableCell>
                    </TableRow>
                  </TableBody>
                )}
              </Table>
            </TableContainer>
          </Scrollbar>

          <Pagination
            count={CategoryTotalPages}
            variant="outlined"
            shape="rounded"
            page={CategoryCurrentPage}
            onChange={handleChangePage}
          />
        </Card>
      </Container>

      <CreateCategory open={open} setOpen={setOpen} />
    </Page>
  );
}
