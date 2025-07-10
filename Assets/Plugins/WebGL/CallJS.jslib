mergeInto(LibraryManager.library, {
    CallExternalJS: function(messagePtr) {
        var message = UTF8ToString(messagePtr); // Convert Unity string pointer to a JavaScript string
        window.CallExternalJS(message);
    }
});
