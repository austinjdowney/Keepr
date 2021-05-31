import { AppState } from '../AppState'
import { api } from './AxiosService'

class KeepsService {
  async getAllKeeps() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data
  }

  async getKeepById(keepId) {
    const res = await api.get(`api/keeps/${keepId}`)
    AppState.keeps = res.data
  }

  async getKeepsByProfileId(profileId) {
    const res = await api.get(`api/profiles/${profileId}/keeps`)
    AppState.keeps = res.data
  }

  async createKeep(data) {
    const res = await api.post('api/keeps', data)
    this.getKeepsByProfileId(res.data.creatorId)
  }

  async deleteKeep(id, profileId) {
    await api.delete(`api/keeps/${id}`)
    this.getKeepsByProfileId(profileId)
  }

  async editKeep(newKeep) {
    await api.put(`api/keeps/${newKeep.id}`, newKeep)
    this.getAllKeeps()
  }
}

export const keepsService = new KeepsService()
