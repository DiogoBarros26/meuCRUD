<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'

const API_URL = 'https://localhost:7061/api/contacts'

const contatos = ref([])
const novoNome = ref('')
const novoEmail = ref('')
const novoPhone = ref('')
const contatoEditando = ref(null)

const buscarContatos = async () => {
  try {
    const response = await axios.get(API_URL)
    contatos.value = response.data
  } catch (error) {
    console.error('Erro ao buscar contatos:', error)
  }
}

const criarContato = async () => {
  try {
    await axios.post(API_URL, {
  name: novoNome.value,
  email: novoEmail.value,
  phone: novoPhone.value
    })

    novoEmail.value = ''
    novoNome.value = ''
    novoPhone.value = ''

    await buscarContatos()
  } catch (error) {
    console.error('Erro ao criar contato:', error)
  }
}

const deletarContato = async (id) => {
  try {
    await axios.delete(`${API_URL}/${id}`)
    await buscarContatos()
  } catch (error) {
    console.error('Erro ao deletar:', error)
  }
}

const atualizarContato = async () => {
  try {
    await axios.put(`${API_URL}/${contatoEditando.value.id}`, {
      id: contatoEditando.value.id,
      name: novoNome.value,
      email: novoEmail.value,
      phone: novoPhone.value
    })

    contatoEditando.value = null
    novoNome.value = ''
    novoEmail.value = ''
    novoPhone.value = ''

    await buscarContatos()
  } catch (error) {
    console.error('Erro ao atualizar:', error)
  }
}
const editarContato = (contato) => {
  contatoEditando.value = contato
  novoNome.value = contato.name
  novoEmail.value = contato.email
  novoPhone.value = contato.phone

}

onMounted(() => {
  buscarContatos()
})
</script>

<template>
  <div class="page">
    <h2>Lista de Contatos</h2>

      <div class="form">
    <input v-model="novoNome" placeholder="Nome" />
  <input v-model="novoEmail" placeholder="Email" />
    <input v-model="novoPhone" placeholder="Telefone" />
   <button class="btn"
  @click="contatoEditando ? atualizarContato() : criarContato()">
  {{ contatoEditando ? 'Atualizar' : 'Salvar' }}
</button>
</div>

    <table class="table">
      <thead>
        <tr>
          <th>Nome</th>
          <th>Email</th>
          <th>Telefone</th>
          <th>Ações</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="contato in contatos" :key="contato.id">
        <td>{{ contato.name }}</td>
        <td>{{ contato.email }}</td>
        <td>{{ contato.phone }}</td>
        <td>
            <button @click="editarContato(contato)" class="edit">Editar</button>
            <button @click="deletarContato(contato.id)" class="delete">Excluir</button>
        </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<style scoped>
.page-header {
  margin-bottom: 20px;
}

.form {
  margin-bottom: 20px;
  display: flex;
  gap: 10px;
}

.form input {
  padding: 8px;
  border: 1px solid #ccc;
  border-radius: 6px;
}

.btn {
  background-color: #2563eb;
  color: white;
  border: none;
  padding: 8px 14px;
  border-radius: 6px;
  cursor: pointer;
}
.btn:hover{
  background-color:#092664;
  color: white;
  border: none;
  padding: 8px 14px;
  margin-right: 5px;
  border-radius: 6px;
  cursor: pointer;
  transition: 0.2s;
}


.table {
  width: 100%;
  border-collapse: collapse;
  background: white;
}

.table th,
.table td {
  border: 1px solid #ddd;
  padding: 10px;
  text-align: left;
}
.edit {
  background-color: #f59e0b;
  color: white;
  border: none;
  padding: 6px 10px;
  margin-right: 5px;
  border-radius: 6px;
  cursor: pointer;
  transition: 0.2s;
}

.edit:hover {
  background-color: #d97706;
}

.delete {
  background-color: #ef4444;
  color: white;
  border: none;
  padding: 6px 10px;
  border-radius: 6px;
  cursor: pointer;
  transition: 0.2s;
}

.delete:hover {
  background-color: #dc2626;
}
</style>
