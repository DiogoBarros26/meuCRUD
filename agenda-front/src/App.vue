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
  background-color: #f4f6f8;
}

.layout {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
}

.header {
  background-color: #1e293b;
  color: white;
  padding: 20px;
  text-align: center;
}

.content {
  flex: 1;
  padding: 30px;
}
</style>