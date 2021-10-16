// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.


let arrayId = [];
let arrayC = [];
let arrayP = [];
let inpInicio = document.getElementById("inpInicio");
let btnCompra = document.getElementById("btnCompra");
let frmCompra = document.getElementById("frmCompra");
let spanCant = document.getElementById("spanCant");
spanCant.innerHTML = 0;

function agregar(precio, curso, id) {

    if (arrayId.length == 0) {

        arrayId.push(id);
        arrayC.push(curso);
        arrayP.push(precio);
        spanCant.innerHTML = arrayC.length;

        dibujarTabla();

        localStorage.setItem('cursos', JSON.stringify(arrayC));
        localStorage.setItem('precios', JSON.stringify(arrayP));
        localStorage.setItem('ids', JSON.stringify(arrayId));
        localStorage.setItem('cant', JSON.stringify(arrayC.length));

        document.getElementById("spnCompra").innerHTML = "Curso agregado a tu carrito..";
        btnCompra.click();
    }
    else {

        if (arrayId.includes(id)) {
            document.getElementById("spnCompra").innerHTML = "Ya tienes agregado ese curso en tu carrito..";
            btnCompra.click();
            return;
        }
        else {
            arrayId.push(id);
            arrayC.push(curso);
            arrayP.push(precio);
            spanCant.innerHTML = arrayC.length;

            dibujarTabla();

            localStorage.setItem('cursos', JSON.stringify(arrayC));
            localStorage.setItem('precios', JSON.stringify(arrayP));
            localStorage.setItem('ids', JSON.stringify(arrayId));
            localStorage.setItem('cant', JSON.stringify(arrayC.length));

            document.getElementById("spnCompra").innerHTML = "Curso agregado a tu carrito..";
            btnCompra.click();
        }

    }
}

function eliminar(pos) {

    arrayC.splice(pos, 1);
    arrayP.splice(pos, 1);
    arrayId.splice(pos, 1);
    spanCant.innerHTML = arrayC.length;

    localStorage.setItem('cursos', JSON.stringify(arrayC));
    localStorage.setItem('precios', JSON.stringify(arrayP));
    localStorage.setItem('ids', JSON.stringify(arrayId));
    localStorage.setItem('cant', JSON.stringify(arrayC.length));


    if (arrayC.length == 0 && arrayP.length == 0) {
        document.getElementsByClassName("modal-body")[0].innerHTML = "Tu carrito está vacío";
        localStorage.removeItem('cursos');
        localStorage.removeItem('precios');
        localStorage.removeItem('cant');
        localStorage.removeItem('ids');
    }
    else {
        dibujarTabla();
    }
}

function dibujarTabla() {

    let modal = document.getElementsByClassName("modal-body")[0];
    modal.innerHTML = "";
    let text = "";
    let suma = 0;

    text += "<table class='table table-striped'><thead><tr><th>Producto</th><th style='text-align: center;'>Precio</th><th style='text-align: center;'>Acciones</th></tr></thead><tbody>";
    let i;
    for (i = 0; i < arrayC.length; i++) {
        text += "<tr><td>" + arrayC[i] + "</td><td style='text-align: center;'>" + arrayP[i] + "</td><td style='text-align: center;'><button onclick='eliminar(" + i + ")' class='btn text-danger'><i class='bi bi-trash'></i></button></td></tr>";
        suma += arrayP[i];
    }

    text += "<tr><th>Total</th><th style='text-align: center;'>" + suma + "</th></tr></tbody></table>";

    modal.innerHTML = text;
}

function cargarCarrito() {

    if (localStorage.getItem('cursos') != null) {

        arrayC = JSON.parse(localStorage.getItem('cursos'));
        arrayP = JSON.parse(localStorage.getItem('precios'));
        arrayId = JSON.parse(localStorage.getItem('ids'));
        spanCant.innerHTML = JSON.parse(localStorage.getItem('cant'));

        dibujarTabla();
    }
}

function comprar() {

    if (inpInicio.value == "0") {
        document.getElementById("spnCompra").innerHTML = "Para poder comprar tus cursos debes estar registrado e iniciar sesión..";
        btnCompra.click();
    }
    else {
        if (arrayId.length == 0) {
            document.getElementById("spnCompra").innerHTML = "Todavía no has agregado ningún curso a tu carrito..";
            btnCompra.click();
        }
        else {

            for (let i = 0; i < arrayId.length; i++) {
                let inpCursos = document.createElement("input");
                inpCursos.name = "inpCursos" + i;
                inpCursos.type = "hidden";
                inpCursos.value = arrayId[i];
                frmCompra.appendChild(inpCursos);
            }
            frmCompra.submit();

            vaciarCarrito();

        }
    }
}

function vaciarCarrito() {

    arrayC = [];
    arrayp = [];
    arrayId = [];
    spanCant.innerHTML = arrayC.length;

    document.getElementsByClassName("modal-body")[0].innerHTML = "Tu carrito está vacío";
    localStorage.removeItem('cursos');
    localStorage.removeItem('precios');
    localStorage.removeItem('cant');
    localStorage.removeItem('ids');

}

window.addEventListener('load', cargarCarrito);