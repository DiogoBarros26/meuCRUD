<template>
  <div class="layout">
    <header class="header">
      <h1>Agenda de Contatos</h1>
    </header>

    <main class="content">
      <RouterView />
    </main>
  </div>
</template>

<style>
body {
  margin: 0;
  font-family: Arial, Helvetica, sans-serif;
  background-color: #fdf5e6;
  color: black;
}

.layout {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
}

.header {
  background-color: #5b048d;
  color: rgb(255, 255, 255);
  padding: 20px;
  text-align: center;
}

.content {
  flex: 1;
  padding: 30px;
}
</style>
