getScreenSize();

window.addEventListener("resize", getScreenSize);

function getScreenSize() {
    const w = window.innerWidth;
    const h = window.innerHeight;
    const element = document.getElementById("gameArea");
    element.style.height = h;
    DotNet.invokeMethod("BlazorBird", "GetScreenSize", { Width: w, Height: h })
}
