var coll = document.getElementsByClassName("collapsible");
Array.from(coll).forEach(collapsible => {
  console.log(coll);
  collapsible.addEventListener('click', () => toggleCollapsed());
})


function toggleCollapsed(button, content) {
  console.log(button)
  button.classList.toggle("expanded");
  if (content.style.display === "block") {
    content.style.display = "none";
  }
  else {
    content.style.display = "block";
  }
}

