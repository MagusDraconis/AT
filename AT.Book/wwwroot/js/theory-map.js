window.theoryMap = (function () {
  let cy = null;
  let tooltip = null;

  // Color by theory layer — the book's journey, not by classification.
  const layerColors = {
    Foundations: '#1D4ED8',     // deep blue
    Structure: '#06B6D4',       // cyan
    Information: '#22C55E',     // green
    Cosmology: '#EAB308',       // gold
    Physics: '#F97316',         // orange
    Correspondence: '#A855F7'   // purple
  };

  function ensureTooltip() {
    if (tooltip) return;
    tooltip = document.createElement('div');
    tooltip.id = 'theory-map-tooltip';
    tooltip.style.cssText =
      'position:absolute;display:none;z-index:1000;max-width:280px;' +
      'background:#151A21;border:1px solid #2b323c;border-radius:6px;' +
      'padding:8px 11px;font-size:0.8rem;line-height:1.4;color:#E6EDF3;' +
      'pointer-events:none;font-family:system-ui,sans-serif;';
    document.body.appendChild(tooltip);
  }

  function init(containerId, payload) {
    if (cy) cy.destroy();
    if (typeof cytoscape === 'undefined') return;
    if (window.cytoscapeDagre) cytoscape.use(window.cytoscapeDagre);
    ensureTooltip();

    const parsed = typeof payload === 'string' ? JSON.parse(payload) : payload;

    cy = cytoscape({
      container: document.getElementById(containerId),
      elements: parsed,
      style: [
        {
          selector: 'node',
          style: {
            'label': 'data(label)',
            'background-color': function (ele) { return layerColors[ele.data('layer')] || '#8B949E'; },
            'color': '#E6EDF3',
            'text-wrap': 'wrap',
            'text-valign': 'center',
            'text-halign': 'center'
          }
        },
        {
          selector: 'node[kind = "part"]', // book parts — section headers of the journey
          style: {
            'shape': 'round-rectangle',
            'background-opacity': 0.9,
            'border-width': 2,
            'border-color': function (ele) { return layerColors[ele.data('layer')] || '#8B949E'; },
            'font-weight': 'bold',
            'font-size': 13,
            'padding': '14px',
            'text-valign': 'center',
            'text-halign': 'center'
          }
        },
        {
          selector: 'node[kind = "chapter"]', // chapters
          style: {
            'shape': 'ellipse',
            'width': 132,
            'height': 38,
            'font-size': 9,
            'background-opacity': 0.9,
            'border-width': 1,
            'border-color': function (ele) { return layerColors[ele.data('layer')] || '#8B949E'; }
          }
        },
        {
          selector: 'node[kind = "object"]', // research objects (dependency network only)
          style: {
            'shape': 'ellipse',
            'width': 130,
            'height': 34,
            'font-size': 9,
            'background-opacity': 0.7,
            'border-width': 1,
            'border-style': 'dashed',
            'border-color': function (ele) { return layerColors[ele.data('layer')] || '#8B949E'; }
          }
        },
        {
          selector: 'edge',
          style: {
            'width': 1.2,
            'line-color': '#3b4252',
            'target-arrow-color': '#3b4252',
            'target-arrow-shape': 'triangle',
            'curve-style': 'bezier',
            'arrow-scale': 0.7
          }
        },
        {
          selector: 'edge[kind = "spine"]',
          style: {
            'width': 2.5,
            'line-color': '#5b6472',
            'target-arrow-color': '#5b6472',
            'line-style': 'dashed'
          }
        },
        {
          selector: 'edge[kind = "dependency"]',
          style: {
            'line-color': '#3b4252',
            'target-arrow-color': '#3b4252',
            'line-style': 'dotted',
            'width': 1
          }
        }
      ],
      layout: {
        name: 'dagre',
        rankDir: 'TB',
        nodeSep: 24,
        rankSep: 48,
        animate: false
      }
    });

    cy.on('tap', 'node', function (e) {
      const url = e.target.data('url');
      if (url) window.location.href = url;
    });

    cy.on('mouseover', 'node', function (e) {
      const summary = e.target.data('summary');
      if (summary) {
        tooltip.innerHTML = summary;
        tooltip.style.display = 'block';
      }
    });
    cy.on('mousemove', 'node', function (e) {
      if (tooltip.style.display === 'block') {
        tooltip.style.left = (e.originalEvent.clientX + 14) + 'px';
        tooltip.style.top = (e.originalEvent.clientY + 14) + 'px';
      }
    });
    cy.on('mouseout', 'node', function () {
      tooltip.style.display = 'none';
    });

    cy.fit(undefined, 30);
  }

  function destroy() { if (cy) { cy.destroy(); cy = null; } }

  return { init, destroy };
})();
