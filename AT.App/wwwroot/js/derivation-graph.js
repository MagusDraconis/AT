// Mermaid-based derivation graph rendering for the AT derivation map.
// The graph definition is generated in C# (DerivationGraphService) and passed
// here as a mermaid "flowchart" string. Node clicks are routed back to Blazor.
window.atDerivationGraph = {
  _dotnetRef: null,

  initialize: async function () {
    if (window.mermaid && !window.mermaid.initialized) {
      window.mermaid.initialize({
        startOnLoad: false,
        securityLevel: 'loose',
        theme: 'dark',
        themeVariables: {
          darkMode: true,
          background: '#0E1116',
          primaryColor: '#151A21',
          primaryTextColor: '#E6EDF3',
          primaryBorderColor: '#3A4149',
          lineColor: '#8B949E',
          secondaryColor: '#10141B',
          tertiaryColor: '#0C0F14',
          clusterBkg: '#10141B',
          clusterBorder: '#3A4149',
          fontFamily: 'Segoe UI, sans-serif'
        },
        flowchart: {
          curve: 'basis',
          nodeSpacing: 42,
          rankSpacing: 58,
          htmlLabels: true
        }
      });
      window.mermaid.initialized = true;
    }
  },

  render: async function (elementId, definition, dotnetRef) {
    await this.initialize();
    this._dotnetRef = dotnetRef;
    const container = document.getElementById(elementId);
    if (!container) return;
    container.innerHTML = '<div class="graph-loading">Rendering derivation graph…</div>';

    const uniqueId = 'at-graph-' + Date.now();
    try {
      const { svg } = await window.mermaid.render(uniqueId, definition);
      container.innerHTML = svg;

      // Route node clicks back to Blazor.
      container.querySelectorAll('g.node').forEach((g) => {
        const id = g.getAttribute('id');
        const match = id ? id.match(/flowchart-([^-]+)-/) : null;
        if (match && this._dotnetRef) {
          g.style.cursor = 'pointer';
          g.addEventListener('click', (e) => {
            e.stopPropagation();
            this._dotnetRef.invokeMethodAsync('OnGraphNodeClick', match[1]);
          });
        }
      });

      // Tooltips: mermaid title elements already carry the node label;
      // use them as native tooltips.
    } catch (err) {
      container.innerHTML = '<div class="graph-error">Graph rendering failed: ' +
        (err && err.message ? err.message : String(err)) + '</div>';
      console.error('Mermaid render error', err);
    }
  },

  highlight: async function (elementId, nodeIds) {
    const container = document.getElementById(elementId);
    if (!container) return;
    container.querySelectorAll('g.node').forEach((g) => {
      const id = g.getAttribute('id');
      const match = id ? id.match(/flowchart-([^-]+)-/) : null;
      if (match && nodeIds.includes(match[1])) {
        g.classList.add('at-node-highlight');
      } else {
        g.classList.remove('at-node-highlight');
      }
    });
  },

  // Zoom of the rendered SVG inside its scroll container. The SVG is sized
  // from its viewBox * scale, so the container scrolls naturally at the
  // zoomed size. Panning is implemented as container scroll (drag below).
  setZoom: function (elementId, scale) {
    const container = document.getElementById(elementId);
    if (!container) return scale;
    const svg = container.querySelector('svg');
    if (!svg) return scale;
    const viewBox = svg.viewBox && svg.viewBox.baseVal;
    const vbW = viewBox && viewBox.width > 0 ? viewBox.width : svg.getBoundingClientRect().width;
    const vbH = viewBox && viewBox.height > 0 ? viewBox.height : svg.getBoundingClientRect().height;
    svg.style.width = (vbW * scale) + 'px';
    svg.style.height = (vbH * scale) + 'px';
    svg.style.maxWidth = 'none';
    svg.removeAttribute('width');
    svg.removeAttribute('height');
    return scale;
  },

  zoomBy: function (elementId, factor) {
    const container = document.getElementById(elementId);
    if (!container) return;
    const svg = container.querySelector('svg');
    if (!svg) return;
    const current = svg.getAttribute('data-at-zoom');
    const base = current ? parseFloat(current) : 1;
    const next = Math.min(4, Math.max(0.15, base * factor));
    svg.setAttribute('data-at-zoom', String(next));
    this.setZoom(elementId, next);
  },

  resetZoom: function (elementId) {
    const container = document.getElementById(elementId);
    if (!container) return;
    const svg = container.querySelector('svg');
    if (!svg) return;
    svg.removeAttribute('data-at-zoom');
    this.setZoom(elementId, 1);
  },

  fitToWidth: function (elementId) {
    const container = document.getElementById(elementId);
    if (!container) return;
    const svg = container.querySelector('svg');
    if (!svg) return;
    const viewBox = svg.viewBox && svg.viewBox.baseVal;
    const vbW = viewBox && viewBox.width > 0 ? viewBox.width : svg.getBoundingClientRect().width;
    const availW = container.clientWidth;
    if (vbW <= 0 || availW <= 0) return;
    const scale = Math.min(1, availW / vbW);
    svg.setAttribute('data-at-zoom', String(scale));
    this.setZoom(elementId, scale);
  },

  // Drag-to-pan: mousedown + mousemove pans the container's scroll position.
  // A movement threshold distinguishes a drag from a plain click; after a
  // drag the imminent click is suppressed so node-detail clicks still work
  // for short clicks but not after panning.
  enablePan: function (elementId) {
    const container = document.getElementById(elementId);
    if (!container || container.getAttribute('data-at-pan')) return;
    container.setAttribute('data-at-pan', 'true');

    let drag = null;
    let suppressClick = false;

    container.addEventListener('mousedown', (e) => {
      if (e.button !== 0) return;
      drag = { x: e.clientX, y: e.clientY, moved: false };
      container.classList.add('at-grabbing');
    });

    window.addEventListener('mousemove', (e) => {
      if (!drag) return;
      const dx = e.clientX - drag.x;
      const dy = e.clientY - drag.y;
      if (!drag.moved && Math.abs(dx) + Math.abs(dy) < 4) return;
      drag.moved = true;
      container.scrollLeft -= dx;
      container.scrollTop -= dy;
      drag.x = e.clientX;
      drag.y = e.clientY;
    });

    window.addEventListener('mouseup', () => {
      if (drag && drag.moved) suppressClick = true;
      drag = null;
      container.classList.remove('at-grabbing');
    });

    // Capture-phase click handler: swallow the click that follows a drag.
    container.addEventListener('click', (e) => {
      if (suppressClick) {
        suppressClick = false;
        e.preventDefault();
        e.stopPropagation();
      }
    }, true);

    // Wheel over the graph zooms (ctrl/meta) or pans vertically (default scroll).
    // We keep the native scroll behaviour but allow ctrl+wheel to zoom.
    container.addEventListener('wheel', (e) => {
      if (!e.ctrlKey && !e.metaKey) return;
      e.preventDefault();
      const factor = e.deltaY < 0 ? 1.15 : 1 / 1.15;
      this.zoomBy(elementId, factor);
    }, { passive: false });
  }
};
