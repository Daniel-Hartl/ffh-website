var coll = document.getElementsByClassName("collapsible");
Array.from(coll).forEach(collapsible => {
  collapsible.addEventListener('click', () => toggleCollapsed());
})


function toggleCollapsed(button, content) {
  button.classList.toggle("expanded");
  if (content.style.display === "block") {
    content.style.display = "none";
  }
  else {
    content.style.display = "block";
  }
}

function collapseAll() {
  var colls = document.getElementsByClassName("collapsible");
  var contents = document.getElementsByClassName("collapsible-content");
  Array.from(colls).forEach(content => { content.classList.remove("expanded"); });
  Array.from(contents).forEach(collapsible => { collapsible.style.display = "none"; });
}

