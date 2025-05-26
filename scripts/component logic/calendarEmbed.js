function loadStreetView() {
    maps = document.createElement("iframe");
    maps.src = "https://calendar.google.com/calendar/embed?src=ebe12d2c011447883173f987a7608c5ec24f4511ad317b3bd27a857e57acb8d4%40group.calendar.google.com&ctz=Europe%2FBerlin";
    maps.height = "100%";
    maps.width = "100%";
    document.getElementById("placeholder").replaceWith(maps);
}