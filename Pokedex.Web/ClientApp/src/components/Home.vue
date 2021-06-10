<template>
    <div>
      <pokemon-type-tag></pokemon-type-tag>
    </div>
</template>

<script>
import PokemonTypeTag from './Pokemon/PokemonTypeTag'
export default {
  name: 'Home',
  props: {
    msg: String
  },
  components:{
    PokemonTypeTag
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
