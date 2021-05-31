import { AppState } from '../AppState'
import { api } from './AxiosService'

class VaultsService {
  async getVaultById(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}`)
    AppState.vaults = res.data
  }

  async getVaultsByProfileId(profileId) {
    const res = await api.get(`api/profiles/${profileId}/vaults`)
    AppState.vaults = res.data
  }

  async createVaults(data) {
    const res = await api.post('api/vaults', data)
    this.getVaultsByProfileId(res.data.creatorId)
  }

  async deleteVault(id, profileId) {
    await api.delete(`api/vaults/${id}`)
    this.getVaultsByProfileId(profileId)
  }

  async editVault(newVault) {
    await api.put(`api/vaults/${newVault.id}`, newVault)
    this.getVaultsByProfileId()
  }
}
export const vaultsService = new VaultsService()
