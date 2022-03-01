const parseObjectToUrlQuery = (obj) => {
    const query = Object.keys(obj)
      .map((key) => `${encodeURIComponent(key)}=${encodeURIComponent(obj[key])}`)
      .join("&");
    return "?" + query;
  };
  
export default parseObjectToUrlQuery;