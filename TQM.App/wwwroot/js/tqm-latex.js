window.TqmLatex = {
  render(root) {
    if (!root || !window.renderMathInElement) {
      return;
    }

    root.querySelectorAll(".katex").forEach((node) => node.remove());
    window.renderMathInElement(root, {
      delimiters: [
        { left: "$$", right: "$$", display: true },
        { left: "$", right: "$", display: false },
        { left: "\\(", right: "\\)", display: false },
      ],
      throwOnError: false
    });
  }
};
