window.theoryGraph = (function () {
  let cy = null;

  const nodeColors = {
    Derived: '#64B5F6',
    Emergent: '#81C784',
    Boundary: '#FFB74D',
    Correspondence: '#CE93D8',
    NewPrimitive: '#E57373',
    Refuted: '#90A4AE',
    Postulated: '#B0BEC5',
    Partial: '#FFF176'
  };

  function init(containerId, elements, dark) {
    if (cy) cy.destroy();
    if (typeof cytoscape === 'undefined') return;
    const parsed = typeof elements === 'string' ? JSON.parse(elements) : elements;
    cy = cytoscape({
      container: document.getElementById(containerId),
      elements: parsed,
      style: [
        {
          selector: 'node',
          style: {
            'label': 'data(label)',
            'width': 26,
            'height': 26,
            'background-color': function (ele) { return nodeColors[ele.data('classification')] || '#90A4AE'; },
            'color': dark ? '#E6EDF3' : '#111',
            'font-size': 8,
            'text-valign': 'bottom',
            'text-margin-y': 5,
            'text-wrap': 'wrap',
            'text-max-width': 70
          }
        },
        {
          selector: 'edge',
          style: {
            'width': 1.4,
            'line-color': dark ? '#3b4252' : '#ccc',
            'target-arrow-color': dark ? '#3b4252' : '#ccc',
            'target-arrow-shape': 'triangle',
            'curve-style': 'bezier'
          }
        },
        { selector: '.dim', style: { 'opacity': 0.08 } }
      ],
      layout: { name: 'breadthfirst', directed: true, padding: 20, spacingFactor: 1.15 }
    });
  }

  function fit() { if (cy) cy.fit(undefined, 40); }

  function closure(id, direction) {
    if (!cy) return;
    const root = cy.getElementById(id);
    const acc = cy.collection().union(root);
    let frontier = direction === 'up' ? root.incomers() : root.outgoers();
    while (frontier.nonempty()) {
      acc.merge(frontier);
      const next = direction === 'up' ? frontier.incomers() : frontier.outgoers();
      frontier = next.difference(acc);
    }
    cy.elements().addClass('dim');
    acc.removeClass('dim');
  }

  function reset() { if (cy) cy.elements().removeClass('dim'); }

  return { init, fit, upstream: function (id) { closure(id, 'up'); }, downstream: function (id) { closure(id, 'down'); }, reset };
})();
