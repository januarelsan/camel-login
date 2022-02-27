mergeInto(LibraryManager.library, {

  Hello: function () {
    window.alert("Hello, Januar You Are Great!");
  },

  MaskingURL: function () {
    if(history.replaceState) history.replaceState({}, "", "/");
  },

  OpenURL: function (url) {
    window.open(Pointer_stringify(url), '_self');
  }
 

});