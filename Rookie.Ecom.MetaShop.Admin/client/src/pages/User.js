import { filter } from "lodash";
import { sentenceCase } from "change-case";
import { useState } from "react";
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
  Box,
  CircularProgress,
} from "@mui/material";
// components
import Page from "../components/Page";
import Label from "../components/Label";
import Scrollbar from "../components/Scrollbar";
import Iconify from "../components/Iconify";
import SearchNotFound from "../components/SearchNotFound";
import {
  UserListHead,
  UserListToolbar,
  UserMoreMenu,
} from "../sections/@dashboard/user";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { getAllUsersAsync } from "../features/userSlice";
import parseObjectToUrlQuery from "src/utils/parseObjectToUrlQuery";
import { LIMIT_USER_PER_PAGE } from "src/app/constants";
import { useNavigate, useSearchParams } from "react-router-dom";
// ----------------------------------------------------------------------

const TABLE_HEAD = [
  { id: "username", label: "Username", alignRight: false },
  { id: "email", label: "Email", alignRight: false },
  { id: "phoneNumber", label: "Phone Number", alignRight: false },
  { id: "province", label: "Province", alignRight: false },
  { id: "country", label: "Country", alignRight: false },
  { id: "role", label: "Roles", alignRight: false },
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
      (_user) => _user.name.toLowerCase().indexOf(query.toLowerCase()) !== -1
    );
  }
  return stabilizedThis.map((el) => el[0]);
}

export default function User() {
  const dispatch = useDispatch();
  const [searchParams] = useSearchParams();
  const {
    users: UserList,
    totalPages: UserTotalPages,
    currentPage: UserCurrentPage,
    loading: UserLoading,
  } = useSelector((state) => state.user);
  const navigate = useNavigate();
  const [page, setPage] = useState(0);
  const [order, setOrder] = useState("asc");
  const [selected, setSelected] = useState([]);
  const [orderBy, setOrderBy] = useState("name");
  const [filterName, setFilterName] = useState("");
  const [rowsPerPage, setRowsPerPage] = useState(LIMIT_USER_PER_PAGE);

  const handleRequestSort = (event, property) => {
    const isAsc = orderBy === property && order === "asc";
    setOrder(isAsc ? "desc" : "asc");
    setOrderBy(property);
  };

  const handleSelectAllClick = (event) => {
    if (event.target.checked) {
      const newSelecteds = UserList.map((n) => n.name);
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
      pathname: "/dashboard/user",
      search: parseObjectToUrlQuery({
        page: newPage,
        limit: rowsPerPage,
      }),
    });
  };

  const handleChangeRowsPerPage = (event) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  const handleFilterByName = (event) => {
    setFilterName(event.target.value);
  };

  const emptyRows =
    page > 0 ? Math.max(0, (1 + page) * rowsPerPage - UserList.length) : 0;

  const filteredUsers = applySortFilter(
    UserList,
    getComparator(order, orderBy),
    filterName
  );

  const isUserNotFound = filteredUsers.length === 0;

  useEffect(() => {
    dispatch(
      getAllUsersAsync(
        parseObjectToUrlQuery({
          page: parseInt(searchParams.get("page")) || UserCurrentPage,
          limit: parseInt(searchParams.get("limit")) || LIMIT_USER_PER_PAGE,
        })
      )
    );
  }, [dispatch, searchParams, UserCurrentPage]);

  useEffect(() => {
    if (!searchParams.get("page") && !searchParams.get("limit")) {
      navigate({
        pathname: "/dashboard/user",
        search: parseObjectToUrlQuery({
          page: 1,
          limit: LIMIT_USER_PER_PAGE,
        }),
      });
    }
    console.log("re-render User");
  }, [searchParams, navigate]);

  console.log(UserList);

  return (
    <Page title="User">
      <Container>
        <Stack
          direction="row"
          alignItems="center"
          justifyContent="space-between"
          mb={LIMIT_USER_PER_PAGE}
        >
          <Typography variant="h4" gutterBottom>
            User
          </Typography>
          <Button
            variant="contained"
            to="#"
            startIcon={<Iconify icon="eva:plus-fill" />}
          >
            New User
          </Button>
        </Stack>

        <Card>
          <UserListToolbar
            numSelected={selected.length}
            filterName={filterName}
            onFilterName={handleFilterByName}
          />

          <Scrollbar>
            <TableContainer sx={{ minWidth: 800 }}>
              <Table>
                <UserListHead
                  order={order}
                  orderBy={orderBy}
                  headLabel={TABLE_HEAD}
                  rowCount={UserList.length}
                  numSelected={selected.length}
                  onRequestSort={handleRequestSort}
                  onSelectAllClick={handleSelectAllClick}
                />
                <TableBody>
                  {UserLoading ? (
                    <TableRow>
                      <TableCell>
                        <Box sx={{ display: "flex" }}>
                          <CircularProgress />
                        </Box>
                      </TableCell>
                    </TableRow>
                  ) : (
                    filteredUsers
                      .slice(
                        page * rowsPerPage,
                        page * rowsPerPage + rowsPerPage
                      )
                      .map((row) => {
                        const {
                          userName,
                          email,
                          phoneNumber,
                          country,
                          province,
                          id,
                          roles
                        } = row;
                        const isItemSelected = selected.indexOf(userName) !== -1;

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
                                onChange={(event) => handleClick(event, userName)}
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
                                  {userName}
                                </Typography>
                              </Stack>
                            </TableCell>
                            <TableCell align="left">{email}</TableCell>
                            <TableCell align="left">{phoneNumber}</TableCell>
                            <TableCell align="left">{province}</TableCell>
                            <TableCell align="left">{country}</TableCell>
                            <TableCell align="left">
                              {
                                roles["$values"]?.map((role, indx) => (
                                  <Label
                                  variant="ghost"
                                  color={
                                    "success"
                                  }
                                  key={indx}
                                >
                                  {sentenceCase(role)}
                                </Label>
                                ))
                              }
                            
                            </TableCell>
                            {/* <TableCell align="left">
                              {isVerified ? "Yes" : "No"}
                            </TableCell>
                            <TableCell align="left">
                              <Label
                                variant="ghost"
                                color={
                                  (status === "banned" && "error") || "success"
                                }
                              >
                                {sentenceCase(status)}
                              </Label>
                            </TableCell> */}

                            <TableCell align="right">
                              <UserMoreMenu />
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
                {isUserNotFound && (
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
            count={UserTotalPages}
            variant="outlined"
            shape="rounded"
            page={UserCurrentPage}
            onChange={handleChangePage}
          />
        </Card>
      </Container>
    </Page>
  );
}
