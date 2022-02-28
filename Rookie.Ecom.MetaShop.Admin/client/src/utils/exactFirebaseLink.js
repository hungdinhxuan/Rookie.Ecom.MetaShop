 const exactFirebaseLink = (link) => {
     try {
         return link.match(/o\/.*\?/g)[0].split('o/')[1].split('?')[0];
     } catch (error) {
         return null;
     }
}

export default exactFirebaseLink;