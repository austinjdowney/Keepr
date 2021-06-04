import { AppState } from '../AppState'
import { api } from './AxiosService'

class VaultKeepsService {
  async getAllVaultKeeps() {
    const res = await api.get('api/vaultkeeps')
    AppState.vaultKeeps = res.data
  }

  async getVaultKeepsById(vaultkeepId) {
    const res = await api.get(`api/vaultkeeps/${vaultkeepId}`)
    AppState.keeps = res.data
  }

  async getKeepsByVaultId(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    AppState.keeps = res.data
  }

  async createVaultKeep(data) {
    const res = await api.post('api/vaultkeeps', data)
    this.getVaultKeepsById(res.data.creatorId)
  }

  async deleteVaultKeep(id, vaultId) {
    await api.delete(`api/vaultkeeps/${id}`)
    this.getKeepsByVaultId(vaultId)
    // this.getAllVaultKeeps(vaultId)
  }
}
export const vaultKeepsService = new VaultKeepsService()
