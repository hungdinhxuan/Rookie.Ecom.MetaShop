 const uuid = (str) => {
  let uuid = "";
  const chars = "0123456789abcdef".split("");
  for (let i = 0; i < 36; i++) {
    uuid += chars[Math.floor(Math.random() * 16)];
  }
  return uuid;
};

export default uuid;