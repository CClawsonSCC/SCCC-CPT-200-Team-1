function signOut (){
    window.localStorage.clear();
    window.location.reload(true);
    window.location.replace('/');
}