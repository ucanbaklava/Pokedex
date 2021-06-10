export default {
    sortByDesc(item){
        return item.sort((a, b) => a.id < b.id ? 1 : (a.id > b.id ? -1 : 0))
    },
    sortByAsc(item){
        return item.sort((a, b) => a.id < b.id ? -1 : (a.id > b.id ? 1 : 0))
    }    
}