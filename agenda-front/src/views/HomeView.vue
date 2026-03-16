<script setup>

import { ref, onMounted } from 'vue'
import axios from 'axios'

import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Button from 'primevue/button'
import InputText from 'primevue/inputtext'

const contatos = ref([])
const editandoId = ref(null)
const novoNome = ref('')
const novoEmail = ref('')
const novoPhone = ref('')

const API = "https://localhost:7061/api/Contacts"

async function buscarContatos() {
  const response = await axios.get(API)
  contatos.value = response.data
}

async function criarContato() {

  if (editandoId.value) {

    await axios.put(`${API}/${editandoId.value}`, {
      id: editandoId.value,
      name: novoNome.value,
      email: novoEmail.value,
      phone: novoPhone.value
    })

  } else {

    await axios.post(API, {
      name: novoNome.value,
      email: novoEmail.value,
      phone: novoPhone.value
    })

  }

  novoNome.value = ''
  novoEmail.value = ''
  novoPhone.value = ''
  editandoId.value = null

  buscarContatos()
}

async function deletarContato(id) {
  await axios.delete(`${API}/${id}`)
  buscarContatos()
}

function editarContato(contato) {
  novoNome.value = contato.name
  novoEmail.value = contato.email
  novoPhone.value = contato.phone

  editandoId.value = contato.id
}

onMounted(() => {
  buscarContatos()
})

</script>

<template>
  <div class="card">

    <h2>Contatos</h2>

    <div class="p-fluid grid mb-3">
      <div class="col-4">
        <InputText v-model="novoNome" placeholder="Nome" />
      </div>

      <div class="col-4">
        <InputText v-model="novoEmail" placeholder="Email" />
      </div>

      <div class="col-4">
        <InputText v-model="novoPhone" placeholder="Telefone" />
      </div>

      <div class="col-12 mt-2">
        <Button
  :label="editandoId ? 'Atualizar' : 'Salvar'"
  :icon="editandoId ? 'pi pi-check' : 'pi pi-plus'"
  @click="criarContato"
/>
      </div>
    </div>

    <DataTable :value="contatos">

      <Column field="name" header="Nome"></Column>

      <Column field="email" header="Email"></Column>

      <Column field="phone" header="Telefone"></Column>

      <Column header="Ações">
        <template #body="slotProps">

          <Button
            icon="pi pi-pencil"
            severity="warning"
            rounded
            class="mr-2"
            @click="editarContato(slotProps.data)"
          />

          <Button
            icon="pi pi-trash"
            severity="danger"
            rounded
            @click="deletarContato(slotProps.data.id)"
          />

        </template>
      </Column>

    </DataTable>

  </div>
</template>
<style scoped>

.page{
  background: linear-gradient(135deg,#f6f9fc,#e9eff5);
  min-height:100vh;
  display:flex;
  justify-content:center;
  align-items:center;
}

.container{
  background:white;
  padding:40px;
  border-radius:16px;
  width:900px;
  box-shadow:0 10px 30px rgba(0,0,0,0.08);
}

.titulo{
  text-align:center;
  margin-bottom:30px;
  font-weight:600;
}

.formulario{
  margin-bottom:30px;
}

.p-inputtext{
  width:100%;
}

.p-button{
  margin-top:10px;
}

</style>
