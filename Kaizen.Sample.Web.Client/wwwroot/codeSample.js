window.highlightCode = function(element) {
    // Find all code blocks within the element and highlight them
    const codeBlocks = element.querySelectorAll('code');
    codeBlocks.forEach(block => {
        if (window.hljs && !block.classList.contains('hljs')) {
            window.hljs.highlightElement(block);
        }
    });
}
