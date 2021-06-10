export default {
    capitalizeFirstLetter(text) {
        return text.charAt(0).toUpperCase() + text.slice(1);
      },
    normalizeText(text) {
        let texts = text.split('-')
        return texts.map(x => this.capitalizeFirstLetter(x)).join(' ')
    }
      
};