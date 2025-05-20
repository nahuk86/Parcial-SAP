const API_BASE = 'https://localhost:32771/api';

async function fetchJson(url) {
  const res = await fetch(url);
  if (!res.ok) throw new Error(res.statusText);
  return res.json();
}

// Carga selects de ubicaciones y usuarios
async function initCreate() {
  const [ubicaciones, usuarios, productos] = await Promise.all([
    fetchJson(`${API_BASE}/ubicaciones`),
    fetchJson(`${API_BASE}/usuarios`),
    fetchJson(`${API_BASE}/productos`)
  ]);

  const sel = (id, data, valField, txtField) => {
    const s = document.getElementById(id);
    s.innerHTML = data
      .map(o => `<option value="${o[valField]}">${o[txtField]}</option>`)
      .join('');
  };
  sel('origen', ubicaciones, 'ubicacionId','nombre');
  sel('destino', ubicaciones, 'ubicacionId','nombre');
  sel('usuario', usuarios, 'usuarioId','nombre');

  // productos para cada detalle row
  window.productos = productos;
  document.querySelectorAll('.producto').forEach(p => {
    p.innerHTML = productos.map(pr =>
      `<option value="${pr.productoId}">${pr.nombre}</option>`
    ).join('');
  });

  // handlers
  document.getElementById('add-detalle').onclick = addDetalleRow;
  document.getElementById('detalles').addEventListener('click', e => {
    if (e.target.classList.contains('remove')) {
      const tbl = document.getElementById('detalles').querySelector('tbody');
      if (tbl.rows.length>1) e.target.closest('tr').remove();
    }
  });

  document.getElementById('transfer-form').onsubmit = async e => {
    e.preventDefault();
    const req = {
      origenId: +document.getElementById('origen').value,
      destinoId: +document.getElementById('destino').value,
      usuarioId: +document.getElementById('usuario').value,
      detalles: Array.from(
        document.getElementById('detalles').querySelectorAll('tbody tr')
      ).map(row => ({
        productoId: +row.querySelector('.producto').value,
        cantidad: +row.querySelector('.cantidad').value
      }))
    };
    try {
      const mov = await fetch(`${API_BASE}/transferencias`, {
        method:'POST',
        headers:{ 'Content-Type':'application/json' },
        body: JSON.stringify(req)
      }).then(r=>{
        if(!r.ok) throw new Error(r.statusText);
        return r.json();
      });
      window.location = `details.html?id=${mov.movimientoHistoricoId}`;
    } catch(err) {
      document.getElementById('message').textContent = 'Error: '+err;
    }
  };
}

// Añade una fila de detalle
function addDetalleRow() {
  const tbody = document.getElementById('detalles').querySelector('tbody');
  const row = tbody.rows[0].cloneNode(true);
  const sel = row.querySelector('.producto');
  sel.innerHTML = window.productos.map(pr =>
    `<option value="${pr.productoId}">${pr.nombre}</option>`
  ).join('');
  row.querySelector('.cantidad').value = 1;
  tbody.appendChild(row);
}

// Carga detalle en details.html
async function initDetails() {
  const params = new URLSearchParams(location.search);
  const id = params.get('id');
  if (!id) { document.getElementById('detalle').textContent='ID no especificado'; return; }
  try {
    const m = await fetchJson(`${API_BASE}/transferencias/${id}`);
    document.getElementById('detalle').innerHTML = `
      <p><strong>ID:</strong> ${m.movimientoHistoricoId}</p>
      <p><strong>Fecha:</strong> ${new Date(m.fecha).toLocaleString()}</p>
      <p><strong>Usuario:</strong> ${m.usuario.nombre}</p>
      <p><strong>Origen:</strong> ${m.origen.nombre}</p>
      <p><strong>Destino:</strong> ${m.destino.nombre}</p>
      <h4>Detalles:</h4>
      <ul>${m.detalles.map(d=>`<li>${d.producto.nombre}: ${d.cantidad}</li>`).join('')}</ul>
    `;
  } catch(err) {
    document.getElementById('detalle').textContent = 'Error: '+err;
  }
}

// Inicialización al cargar cada página
document.addEventListener('DOMContentLoaded', () => {
  if (location.pathname.endsWith('create.html')) initCreate();
  if (location.pathname.endsWith('details.html')) initDetails();
});

async function initStock() {
  try {
    const ubicaciones = await fetchJson(`${API_BASE}/ubicaciones`);
    const container   = document.getElementById('stock-container');
    container.innerHTML = ''; 

    ubicaciones.forEach(u => {
      // Título de la ubicación
      const section = document.createElement('section');
      const h3 = document.createElement('h3');
      h3.textContent = u.nombre;
      section.appendChild(h3);

      // Tabla de stock
      const table = document.createElement('table');
      table.innerHTML = `
        <thead>
          <tr>
            <th>Producto</th>
            <th>Cantidad</th>
          </tr>
        </thead>
        <tbody>
          ${u.stock.map(s =>
            `<tr>
              <td>${s.producto.nombre}</td>
              <td>${s.cantidad}</td>
            </tr>`
          ).join('')}
        </tbody>
      `;
      section.appendChild(table);
      container.appendChild(section);
    });
  } catch(err) {
    document.getElementById('stock-container').textContent = 'Error cargando stock: ' + err;
  }
}

// Al inicio, detecta stock.html
document.addEventListener('DOMContentLoaded', () => {
  if (location.pathname.endsWith('create.html')) initCreate();
  else if (location.pathname.endsWith('details.html')) initDetails();
  else if (location.pathname.endsWith('stock.html')) initStock();
});
