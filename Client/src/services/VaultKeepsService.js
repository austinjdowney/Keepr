import { AppState } from '../AppState'
import { api } from './AxiosService'

class VaultKeepsService {
  async getAllVaultKeeps() {
    const res = await api.get('api/vaultkeeps')
    AppState.vaultkeeps = res.data
  }

  async getVaultKeepsById(vaultkeepId) {
    const res = await api.get(`api/vaultkeeps/${vaultkeepId}`)
    AppState.vaultkeeps = res.data
  }

  async createVaultKeep(data) {
    const res = await api.post('api/vaultkeeps', data)
    this.getVaultKeepsById(res.data.creatorId)
  }

  async deleteVaultKeep(id, profileId) {
    await api.delete(`api/vaultkeeps/${id}`)
    this.getVaultKeepsById(profileId)
  }
}
export const vaultKeepsService = new VaultKeepsService()
